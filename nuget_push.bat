set mypath=%cd%
echo %mypath%

set SEARCH_BASE="MeaMod.Utilities\bin\Release"
set FILTER=*.DLL
for /r %SEARCH_BASE% %%i in (%FILTER%) do (
signtool verify /pa /q "%%i" 1>nul 2>nul 
if errorlevel 1 (
signtool sign /n "James Weston" /fd SHA256 /d "MeaMod.Utilities" /du "https://www.meamod.com" /tr http://timestamp.digicert.com /td sha256 "%%i"
)
)

set SEARCH_BASE="MeaMod.Utilities.Security\bin\Release\netstandard2.0"
set FILTER=*.DLL
for /r %SEARCH_BASE% %%i in (%FILTER%) do (
signtool verify /pa /q "%%i" 1>nul 2>nul 
if errorlevel 1 (
signtool sign /n "James Weston" /fd SHA256 /d "MeaMod.Utilities" /du "https://www.meamod.com" /tr http://timestamp.digicert.com /td sha256 "%%i"
)
)

set SEARCH_BASE="MeaMod.Utilities.UI.WinForms\bin\Release"
set FILTER=*.DLL
for /r %SEARCH_BASE% %%i in (%FILTER%) do (
signtool verify /pa /q "%%i" 1>nul 2>nul 
if errorlevel 1 (
signtool sign /n "James Weston" /fd SHA256 /d "MeaMod.Utilities" /du "https://www.meamod.com" /tr http://timestamp.digicert.com /td sha256 "%%i"
)
)


dotnet pack -c Release --no-build

nuget sign "MeaMod.Utilities\bin\Release\*.nupkg" -CertificateSubjectName "James Weston" -Timestamper http://timestamp.digicert.com -NonInteractive

nuget sign "MeaMod.Utilities.Security\bin\Release\*.nupkg" -CertificateSubjectName "James Weston" -Timestamper http://timestamp.digicert.com -NonInteractive

nuget sign "MeaMod.Utilities.UI.WinForms\bin\Release\*.nupkg" -CertificateSubjectName "James Weston" -Timestamper http://timestamp.digicert.com -NonInteractive

nuget push "MeaMod.Utilities\bin\Release\*.nupkg" -Source nuget.org -SkipDuplicate -NonInteractive

nuget push "MeaMod.Utilities.Security\bin\Release\*.nupkg" -Source nuget.org -SkipDuplicate -NonInteractive

nuget push "MeaMod.Utilities.UI.WinForms\bin\Release\*.nupkg" -Source nuget.org -SkipDuplicate -NonInteractive

move "MeaMod.Utilities\bin\Release\*.nupkg" nugetpackagearchive

move "MeaMod.Utilities.Security\bin\Release\*.nupkg" nugetpackagearchive

move "MeaMod.Utilities.UI.WinForms\bin\Release\*.nupkg" nugetpackagearchive


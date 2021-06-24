nuget sign "MeaMod.Utilities\bin\Release\*.nupkg" -CertificateSubjectName "James Weston" -Timestamper http://timestamp.digicert.com -NonInteractive

nuget push "MeaMod.Utilities\bin\Release\*.nupkg" -Source SimplexGithub -SkipDuplicate -NonInteractive

move "MeaMod.Utilities\bin\Release\*.nupkg" packagearchive
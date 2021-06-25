using System.Runtime.InteropServices;

namespace MeaMod.Utilities
{
    public static class OperatingSystem
    {
        public static bool IsWindows() =>
#if NETFRAMEWORK
           true;
#else
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

#endif

        public static bool IsMacOS() =>
#if NETFRAMEWORK
           false;
#else
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

#endif

        public static bool IsLinux() =>
#if NETFRAMEWORK
           false;
#else
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

#endif

    }
}

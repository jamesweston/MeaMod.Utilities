using System.Runtime.InteropServices;

namespace MeaMod.Utilities
{
    /// <summary>Methods for detecting the operating system running the library</summary>
    public static class OperatingSystem
    {
        /// <summary>
        /// Method to check if running Windows
        /// </summary>
        /// <returns>Boolean true if running Windows</returns>
        public static bool IsWindows() =>
#if NETFRAMEWORK
           true;
#else
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

#endif

        /// <summary>
        /// Method to check if running MacOS
        /// </summary>
        /// <returns>Boolean true if running MacOS</returns>
        public static bool IsMacOS() =>
#if NETFRAMEWORK
           false;
#else
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

#endif


        /// <summary>
        /// Method to check if running Linux
        /// </summary>
        /// <returns>Boolean true if running Linux</returns>
        public static bool IsLinux() =>
#if NETFRAMEWORK
           false;
#else
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

#endif

    }
}

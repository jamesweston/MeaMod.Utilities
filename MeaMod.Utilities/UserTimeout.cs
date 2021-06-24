using System;
using System.Runtime.InteropServices;

namespace MeaMod.Utilities
{
    public class UserTimeout
    {
        private static int idletime;
        private static LASTINPUTINFO lastInputInf = new LASTINPUTINFO();

        [StructLayout(LayoutKind.Sequential)]
        public struct LASTINPUTINFO
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public int dwTime;
        }

        [DllImport("user32.dll")]
        public static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        public static int GetLastInputTime()
        {
            idletime = 0;
            lastInputInf.cbSize = Marshal.SizeOf(lastInputInf);
            lastInputInf.dwTime = 0;
            if (GetLastInputInfo(ref lastInputInf))
            {
                idletime = Environment.TickCount - lastInputInf.dwTime;
            }

            if (idletime > 0)
            {
                return (int)Math.Round(idletime / 1000d);
            }
            else
            {
                return 0;
            }
        }
    }
}
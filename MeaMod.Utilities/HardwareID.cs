using System;
using System.Management;

namespace MeaMod.Utilities
{
    public class HardwareID
    {
        public static string GetHardwareID()
        {
            return Cryptography.Hash.SHA256(GetProcessorId() + GetMotherboardID() + GetDiskVolumeSerialNumber()).ToUpperInvariant();
        }

        // Get processor ID
        public static string GetProcessorId()
        {
            string strProcessorID = string.Empty;
            var query = new SelectQuery("Win32_processor");
            var search = new ManagementObjectSearcher(query);
            foreach (ManagementObject info in search.Get())
                strProcessorID = info["processorID"].ToString();
            return strProcessorID;
        }
        // Get MAC Address
        public static string GetMACAddress()
        {
            var mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            var moc = mc.GetInstances();
            string MacAddress = string.Empty;
            foreach (ManagementObject mo in moc)
            {
                if (MacAddress.Equals(string.Empty))
                {
                    if (bool.Parse((string)mo["IPEnabled"]))
                        MacAddress = mo["MacAddress"].ToString();
                    mo.Dispose();
                }

                MacAddress = MacAddress.Replace(":", string.Empty);
            }

            return MacAddress;
        }

        public static string GetDiskVolumeSerialNumber()
        {
            try
            {
                var _disk = new ManagementObject("Win32_LogicalDisk.deviceid=\"" + Environment.GetEnvironmentVariable("SystemDrive") + "\"");
                _disk.Get();
                return _disk["VolumeSerialNumber"].ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        // Get Motherboard ID
        public static string GetMotherboardID()
        {
            string strMotherboardID = string.Empty;
            var query = new SelectQuery("Win32_BaseBoard");
            var search = new ManagementObjectSearcher(query);
            foreach (ManagementObject info in search.Get())
                strMotherboardID = info["SerialNumber"].ToString();
            return strMotherboardID;
        }
    }
}
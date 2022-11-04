using Microsoft.Win32;
using System;
using System.Runtime.Versioning;

namespace MeaMod.Utilities
{
    /// <summary>The RegistrySettings class contains methods to access the Windows registry for getting, creating, updating and deleting registry values</summary>
    public class RegistrySettings
    {
        /// <summary>This method deletes a value from to the current user registry located in HKCU\<paramref name="BaseKey"/>\<paramref name="AppNameSubKey"/>\<paramref name="ValueName"/></summary>
        /// <param name="BaseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKCU</param>
        /// <param name="AppNameSubKey">Name of the SubKey that <paramref name="ValueName"/> is located in under <paramref name="BaseKey"/></param>
        /// <param name="ValueName">Name of registry value to delete</param>
        public static void DeleteSetting(string BaseKey,string AppNameSubKey, string ValueName)
        {
            try
            {
                var registryKey = Registry.CurrentUser.CreateSubKey(string.Concat(BaseKey, AppNameSubKey));
                registryKey.DeleteValue(ValueName);
                registryKey.Close();
            }
            catch (NullReferenceException)
            {
                // Already removed
            }
            catch (Exception exception1)
            {
                var exception = exception1;
                Console.WriteLine("MeaMod.Utilities.RegistrySettings.DeleteSetting : Exception" + System.Environment.NewLine + exception.Message);
                throw exception;
            }
        }

        /// <summary>This method deletes a value from to the local machine registry located in HKLM\<paramref name="BaseKey"/>\<paramref name="AppNameSubKey"/>\<paramref name="ValueName"/></summary>
        /// <param name="BaseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKLM</param>
        /// <param name="AppNameSubKey">Name of the SubKey that <paramref name="ValueName"/> is located in under <paramref name="BaseKey"/></param>
        /// <param name="ValueName">Name of registry value to delete</param>
        public static void DeleteSettingLM(string BaseKey, string AppNameSubKey, string ValueName)
        {
            try
            {
                var registryKey = Registry.LocalMachine.CreateSubKey(string.Concat(BaseKey, AppNameSubKey));
                registryKey.DeleteValue(ValueName);
                registryKey.Close();
            }
            catch (NullReferenceException)
            {
                // Already removed
            }
            catch (Exception exception1)
            {
                var exception = exception1;
                Console.WriteLine("MeaMod.Utilities.RegistrySettings.DeleteSetting : Exception" + System.Environment.NewLine + exception.Message);
                throw exception;
            }
        }

        /// <summary>This method gets <paramref name="ValueName"/> as a boolean from the current user registry located in HKCU\<paramref name="BaseKey"/>\<paramref name="AppNameSubKey"/>\<paramref name="ValueName"/></summary>
        /// <param name="BaseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKCU</param>
        /// <param name="AppNameSubKey">Name of the SubKey that <paramref name="ValueName"/> is located in under <paramref name="BaseKey"/></param>
        /// <param name="ValueName">Name of value to retrieve</param>
        /// <param name="DefaultValue">Default value if <paramref name="ValueName"/> does not exist</param>
        /// <returns>The boolean from <paramref name="ValueName"/> or <paramref name="DefaultValue"/> if <paramref name="ValueName"/> does not exist</returns>
        public static bool GetBooleanSetting(string BaseKey,string AppNameSubKey, string ValueName, bool DefaultValue = false)
        {
            bool flag = DefaultValue;
            try
            {
                var registryKey = Registry.CurrentUser.OpenSubKey(string.Concat(BaseKey, AppNameSubKey), RegistryKeyPermissionCheck.ReadSubTree);
                flag = Convert.ToBoolean(registryKey.GetValue(ValueName));
                registryKey.Close();
            }
            catch (NullReferenceException)
            {
                return flag;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.GetBooleanSetting : Exception" + System.Environment.NewLine + "", ex.Message));
                throw;
            }

            return flag;
        }

        /// <summary>This method gets <paramref name="ValueName"/> as a boolean from the local machine registry located in HKLM\<paramref name="BaseKey"/>\<paramref name="AppNameSubKey"/>\<paramref name="ValueName"/></summary>
        /// <param name="BaseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKLM</param>
        /// <param name="AppNameSubKey">Name of the SubKey that <paramref name="ValueName"/> is located in under <paramref name="BaseKey"/></param>
        /// <param name="ValueName">Name of value to retrieve</param>
        /// <param name="DefaultValue">Default value if <paramref name="ValueName"/> does not exist</param>
        /// <returns>The boolean from <paramref name="ValueName"/> or <paramref name="DefaultValue"/> if <paramref name="ValueName"/> does not exist</returns>
        public static bool GetBooleanSettingLM(string BaseKey, string AppNameSubKey, string ValueName, bool DefaultValue = false)
        {
            bool flag = DefaultValue;
            try
            {
                var registryKey = Registry.LocalMachine.OpenSubKey(string.Concat(BaseKey, AppNameSubKey), RegistryKeyPermissionCheck.ReadSubTree);
                flag = Convert.ToBoolean(registryKey.GetValue(ValueName));
                registryKey.Close();
            }
            catch (NullReferenceException)
            {
                return flag;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.GetBooleanLMSetting : Exception" + System.Environment.NewLine + "", ex.Message));
                throw;
            }

            return flag;
        }

        /// <summary>This method gets <paramref name="ValueName"/> as a string from the current user registry located in HKCU\<paramref name="BaseKey"/>\<paramref name="AppNameSubKey"/>\<paramref name="ValueName"/></summary>
        /// <param name="BaseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKCU</param>
        /// <param name="AppNameSubKey">Name of the SubKey that <paramref name="ValueName"/> is located in under <paramref name="BaseKey"/></param>
        /// <param name="ValueName">Name of value to retrieve</param>
        /// <returns>Returns string of ValueName</returns>
        /// <returns>The string value from <paramref name="ValueName"/> or empty if <paramref name="ValueName"/> does not exist</returns>

        public static string GetSetting(string BaseKey, string AppNameSubKey, string ValueName)
        {
            string str = "";
            try
            {
                var registryKey = Registry.CurrentUser.OpenSubKey(string.Concat(BaseKey, AppNameSubKey), RegistryKeyPermissionCheck.ReadSubTree);
                str = registryKey.GetValue(ValueName).ToString();
                registryKey.Close();
            }
            catch (NullReferenceException)
            {
                return str;
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.GetSetting : Exception" + System.Environment.NewLine + "", exception.Message));
                throw;
            }

            return str;
        }

        /// <summary>This method gets <paramref name="ValueName"/> as a string from the current user registry located in HKLM\<paramref name="BaseKey"/>\<paramref name="AppNameSubKey"/>\<paramref name="ValueName"/></summary>
        /// <param name="BaseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKLM</param>
        /// <param name="AppNameSubKey">Name of the SubKey that <paramref name="ValueName"/> is located in under <paramref name="BaseKey"/></param>
        /// <param name="ValueName">Name of value to retrieve</param>
        /// <returns>The string value from <paramref name="ValueName"/> or empty if <paramref name="ValueName"/> does not exist</returns>
        public static string GetSettingLM(string BaseKey, string AppNameSubKey, string ValueName)
        {
            string str = "";
            try
            {
                var registryKey = Registry.LocalMachine.OpenSubKey(string.Concat(BaseKey, AppNameSubKey), RegistryKeyPermissionCheck.ReadSubTree);
                str = registryKey.GetValue(ValueName).ToString();
                registryKey.Close();
            }
            catch (NullReferenceException)
            {
                return str;
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.GetSettingLM : Exception" + System.Environment.NewLine + "", exception.Message));
                throw;
            }

            return str;
        }

        /// <summary>This method gets <paramref name="ValueName"/> as a integer from the current user registry located in HKCU\<paramref name="BaseKey"/>\<paramref name="AppNameSubKey"/>\<paramref name="ValueName"/></summary>
        /// <param name="BaseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKCU</param>
        /// <param name="AppNameSubKey">Name of the SubKey that <paramref name="ValueName"/> is located in under <paramref name="BaseKey"/></param>
        /// <param name="ValueName">Name of value to retrieve</param>
        /// <returns>The integer value from <paramref name="ValueName"/> or 0 if <paramref name="ValueName"/> does not exist</returns>
        public static int GetInteger(string BaseKey, string AppNameSubKey, string ValueName)
        {
            int inti = 0;
            try
            {
                var registryKey = Registry.CurrentUser.OpenSubKey(string.Concat(BaseKey, AppNameSubKey), RegistryKeyPermissionCheck.ReadSubTree);
                RegistryValueKind rvk = registryKey.GetValueKind(ValueName);
                switch (rvk)
                {

                    case RegistryValueKind.DWord:
                        inti = (int)registryKey.GetValue(ValueName);
                        break;
                    case RegistryValueKind.QWord:
                        inti = (int)registryKey.GetValue(ValueName);
                        break;
                    case RegistryValueKind.String:
                        inti = int.Parse((string)registryKey.GetValue(ValueName));
                        break;
                }
                registryKey.Close();
            }
            catch (NullReferenceException)
            {
                return inti;
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.GetInteger: Exception" + System.Environment.NewLine + "", exception.Message));
                throw;
            }

            return inti;
        }

        /// <summary>This method gets <paramref name="ValueName"/> as a integer from the local machine registry located in HKLM\<paramref name="BaseKey"/>\<paramref name="AppNameSubKey"/>\<paramref name="ValueName"/></summary>
        /// <param name="BaseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKLM</param>
        /// <param name="AppNameSubKey">Name of the SubKey that <paramref name="ValueName"/> is located in under <paramref name="BaseKey"/></param>
        /// <param name="ValueName">Name of value to retrieve</param>
        /// <returns>The integer value from <paramref name="ValueName"/> or 0 if <paramref name="ValueName"/> does not exist</returns>
        public static int GetIntegerLM(string BaseKey,string AppNameSubKey, string ValueName)
        {
            int inti = 0;
            try
            {
                var registryKey = Registry.LocalMachine.OpenSubKey(string.Concat(BaseKey, AppNameSubKey), RegistryKeyPermissionCheck.ReadSubTree);
                RegistryValueKind rvk = registryKey.GetValueKind(ValueName);
                switch (rvk)
                {

                    case RegistryValueKind.DWord:
                        inti = (int)registryKey.GetValue(ValueName);
                        break;
                    case RegistryValueKind.QWord:
                        inti = (int)registryKey.GetValue(ValueName);
                        break;
                    case RegistryValueKind.String:
                        inti = int.Parse((string)registryKey.GetValue(ValueName));
                        break;
                }
                registryKey.Close();
            }
            catch (NullReferenceException)
            {
                return inti;
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.GetIntegerLM: Exception" + System.Environment.NewLine + "", exception.Message));
                throw;
            }

            return inti;
        }

        /// <summary>This method saves <paramref name="ValueName"/> as a boolean to the current user registry located in HKCU\<paramref name="BaseKey"/>\<paramref name="AppNameSubKey"/>\<paramref name="ValueName"/></summary>
        /// <param name="BaseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKCU</param>
        /// <param name="AppNameSubKey">Name of the SubKey that <paramref name="ValueName"/> is located in under <paramref name="BaseKey"/></param>
        /// <param name="ValueName">Name of value</param>
        /// <param name="Value">Value to be saved as boolean</param>
        public static void SaveBooleanSetting(string BaseKey, string AppNameSubKey, string ValueName, bool Value)
        {
            try
            {
                string str = string.Concat(BaseKey, AppNameSubKey);
                var registryKey = Registry.CurrentUser.CreateSubKey(str);
                registryKey.SetValue(ValueName, Value);
                registryKey.Close();
            }
            catch (Exception exception1)
            {
                var exception = exception1;
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.SaveBooleanSetting : Exception" + System.Environment.NewLine + "", exception.Message));
                throw exception;
            }
        }

        /// <summary>This method saves <paramref name="ValueName"/> as a boolean to the local machine registry located in HKLM\<paramref name="BaseKey"/>\<paramref name="AppNameSubKey"/>\<paramref name="ValueName"/></summary>
        /// <param name="BaseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKLM</param>
        /// <param name="AppNameSubKey">Name of the SubKey that <paramref name="ValueName"/> is located in under <paramref name="BaseKey"/></param>
        /// <param name="ValueName">Name of value</param>
        /// <param name="Value">Value to be saved as boolean</param>
        public static void SaveBooleanSettingLM(string BaseKey, string AppNameSubKey, string ValueName, bool Value)
        {
            try
            {
                string str = string.Concat(BaseKey, AppNameSubKey);
                var registryKey = Registry.LocalMachine.CreateSubKey(str);
                registryKey.SetValue(ValueName, Value);
                registryKey.Close();
            }
            catch (Exception exception1)
            {
                var exception = exception1;
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.SaveBooleanSettingLM : Exception" + System.Environment.NewLine + "", exception.Message));
                throw exception;
            }
        }

        /// <summary>This method saves <paramref name="ValueName"/> as a string to the current user registry located in HKCU\<paramref name="BaseKey"/>\<paramref name="AppNameSubKey"/>\<paramref name="ValueName"/></summary>
        /// <param name="BaseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKCU</param>
        /// <param name="AppNameSubKey">Name of the SubKey that <paramref name="ValueName"/> is located in under <paramref name="BaseKey"/></param>
        /// <param name="ValueName">Name of value</param>
        /// <param name="Value">Value to be saved as string</param>
        public static void SaveSetting(string BaseKey, string AppNameSubKey, string ValueName, string Value)
        {
            try
            {
                string str = string.Concat(BaseKey, AppNameSubKey);
                var registryKey = Registry.CurrentUser.CreateSubKey(str);
                registryKey.SetValue(ValueName, Value, RegistryValueKind.String);
                registryKey.Close();
            }
            catch (Exception exception1)
            {
                var exception = exception1;
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.SaveSetting : Exception" + System.Environment.NewLine + "", exception.Message));
                throw exception;
            }
        }

        /// <summary>This method saves <paramref name="ValueName"/> as a string to the local machine registry located in HKLM\<paramref name="BaseKey"/>\<paramref name="AppNameSubKey"/>\<paramref name="ValueName"/></summary>
        /// <param name="BaseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKLM</param>
        /// <param name="AppNameSubKey">Name of the SubKey that <paramref name="ValueName"/> is located in under <paramref name="BaseKey"/></param>
        /// <param name="ValueName">Name of value</param>
        /// <param name="Value">Value to be saved as string</param>
        public static void SaveSettingLM(string BaseKey, string AppNameSubKey, string ValueName, string Value)
        {
            try
            {
                string str = string.Concat(BaseKey, AppNameSubKey);
                var registryKey = Registry.LocalMachine.CreateSubKey(str);
                registryKey.SetValue(ValueName, Value, RegistryValueKind.String);
                registryKey.Close();
            }
            catch (Exception exception1)
            {
                var exception = exception1;
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.SaveSetting : Exception" + System.Environment.NewLine + "", exception.Message));
                throw exception;
            }
        }

        /// <summary>This method saves <paramref name="ValueName"/> as a integer to the current user registry located in HKCU\<paramref name="BaseKey"/>\<paramref name="AppNameSubKey"/>\<paramref name="ValueName"/></summary>
        /// <param name="BaseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKCU</param>
        /// <param name="AppNameSubKey">Name of the SubKey that <paramref name="ValueName"/> is located in under <paramref name="BaseKey"/></param>
        /// <param name="ValueName">Name of value</param>
        /// <param name="Value">Value to be saved as integer</param>
        public static void SaveInteger(string BaseKey, string AppNameSubKey, string ValueName, int Value)
        {
            try
            {
                string str = string.Concat(BaseKey, AppNameSubKey);
                var registryKey = Registry.CurrentUser.CreateSubKey(str);
                registryKey.SetValue(ValueName, Value, RegistryValueKind.DWord);
                registryKey.Close();
            }
            catch (Exception exception1)
            {
                var exception = exception1;
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.SaveInteger : Exception" + System.Environment.NewLine + "", exception.Message));
                throw exception;
            }
        }

        /// <summary>This method saves <paramref name="ValueName"/> as a integer to the local machine registry located in HKLM\<paramref name="BaseKey"/>\<paramref name="AppNameSubKey"/>\<paramref name="ValueName"/></summary>
        /// <param name="BaseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKLM</param>
        /// <param name="AppNameSubKey">Name of the SubKey that <paramref name="ValueName"/> is located in under <paramref name="BaseKey"/></param>
        /// <param name="ValueName">Name of value</param>
        /// <param name="Value">Value to be saved as integer</param>
        public static void SaveIntegerLM(string BaseKey, string AppNameSubKey, string ValueName, int Value)
        {
            try
            {
                string str = string.Concat(BaseKey, AppNameSubKey);
                var registryKey = Registry.LocalMachine.CreateSubKey(str);
                registryKey.SetValue(ValueName, Value, RegistryValueKind.DWord);
                registryKey.Close();
            }
            catch (Exception exception1)
            {
                var exception = exception1;
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.SaveIntegerLM : Exception" + System.Environment.NewLine + "", exception.Message));
                throw exception;
            }
        }
    }
}
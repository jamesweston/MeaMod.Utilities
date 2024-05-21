using Microsoft.Win32;
using System;

namespace MeaMod.Utilities
{
    /// <summary>The RegistrySettings class contains methods to access the Windows registry for getting, creating, updating and deleting registry values</summary>
    public class RegistrySettings
    {
        /// <summary>This method deletes a value from to the current user registry located in HKCU\<paramref name="baseKey"/>\<paramref name="appNameSubKey"/>\<paramref name="valueName"/></summary>
        /// <param name="baseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKCU</param>
        /// <param name="appNameSubKey">Name of the SubKey that <paramref name="valueName"/> is located in under <paramref name="baseKey"/></param>
        /// <param name="valueName">Name of registry value to delete</param>
        public static void DeleteSetting(string baseKey,string appNameSubKey, string valueName)
        {
            try
            {
                var registryKey = Registry.CurrentUser.CreateSubKey(string.Concat(baseKey, appNameSubKey));
                if (registryKey == null) return;
                registryKey.DeleteValue(valueName);
                registryKey.Close();
            }
            catch (NullReferenceException)
            {
                // Already removed
            }
            catch (Exception exception)
            {
                Console.WriteLine("MeaMod.Utilities.RegistrySettings.DeleteSetting : Exception" + Environment.NewLine + exception.Message);
                throw;
            }
        }

        /// <summary>This method deletes a value from to the local machine registry located in HKLM\<paramref name="baseKey"/>\<paramref name="appNameSubKey"/>\<paramref name="valueName"/></summary>
        /// <param name="baseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKLM</param>
        /// <param name="appNameSubKey">Name of the SubKey that <paramref name="valueName"/> is located in under <paramref name="baseKey"/></param>
        /// <param name="valueName">Name of registry value to delete</param>
        public static void DeleteSettingLm(string baseKey, string appNameSubKey, string valueName)
        {
            try
            {
                var registryKey = Registry.LocalMachine.CreateSubKey(string.Concat(baseKey, appNameSubKey));
                if (registryKey == null) return;
                registryKey.DeleteValue(valueName);
                registryKey.Close();
            }
            catch (NullReferenceException)
            {
                // Already removed
            }
            catch (Exception exception)
            {
                Console.WriteLine("MeaMod.Utilities.RegistrySettings.DeleteSetting : Exception" + Environment.NewLine + exception.Message);
                throw;
            }
        }

        /// <summary>This method gets <paramref name="valueName"/> as a boolean from the current user registry located in HKCU\<paramref name="baseKey"/>\<paramref name="appNameSubKey"/>\<paramref name="valueName"/></summary>
        /// <param name="baseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKCU</param>
        /// <param name="appNameSubKey">Name of the SubKey that <paramref name="valueName"/> is located in under <paramref name="baseKey"/></param>
        /// <param name="valueName">Name of value to retrieve</param>
        /// <param name="defaultValue">Default value if <paramref name="valueName"/> does not exist</param>
        /// <returns>The boolean from <paramref name="valueName"/> or <paramref name="defaultValue"/> if <paramref name="valueName"/> does not exist</returns>
        public static bool GetBooleanSetting(string baseKey,string appNameSubKey, string valueName, bool defaultValue = false)
        {
            bool flag = defaultValue;
            try
            {
                var registryKey = Registry.CurrentUser.OpenSubKey(string.Concat(baseKey, appNameSubKey), RegistryKeyPermissionCheck.ReadSubTree);
                if (registryKey != null)
                {
                    flag = Convert.ToBoolean(registryKey.GetValue(valueName));
                    registryKey.Close();
                }
            }
            catch (NullReferenceException)
            {
                return flag;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.GetBooleanSetting : Exception" + Environment.NewLine + "", ex.Message));
                throw;
            }

            return flag;
        }

        /// <summary>This method gets <paramref name="valueName"/> as a boolean from the local machine registry located in HKLM\<paramref name="baseKey"/>\<paramref name="appNameSubKey"/>\<paramref name="valueName"/></summary>
        /// <param name="baseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKLM</param>
        /// <param name="appNameSubKey">Name of the SubKey that <paramref name="valueName"/> is located in under <paramref name="baseKey"/></param>
        /// <param name="valueName">Name of value to retrieve</param>
        /// <param name="defaultValue">Default value if <paramref name="valueName"/> does not exist</param>
        /// <returns>The boolean from <paramref name="valueName"/> or <paramref name="defaultValue"/> if <paramref name="valueName"/> does not exist</returns>
        public static bool GetBooleanSettingLm(string baseKey, string appNameSubKey, string valueName, bool defaultValue = false)
        {
            bool flag = defaultValue;
            try
            {
                var registryKey = Registry.LocalMachine.OpenSubKey(string.Concat(baseKey, appNameSubKey), RegistryKeyPermissionCheck.ReadSubTree);
                if (registryKey != null)
                {
                    flag = Convert.ToBoolean(registryKey.GetValue(valueName));
                    registryKey.Close();
                }
            }
            catch (NullReferenceException)
            {
                return flag;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.GetBooleanLMSetting : Exception" + Environment.NewLine + "", ex.Message));
                throw;
            }

            return flag;
        }

        /// <summary>This method gets <paramref name="valueName"/> as a string from the current user registry located in HKCU\<paramref name="baseKey"/>\<paramref name="appNameSubKey"/>\<paramref name="valueName"/></summary>
        /// <param name="baseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKCU</param>
        /// <param name="appNameSubKey">Name of the SubKey that <paramref name="valueName"/> is located in under <paramref name="baseKey"/></param>
        /// <param name="valueName">Name of value to retrieve</param>
        /// <returns>Returns string of ValueName</returns>
        /// <returns>The string value from <paramref name="valueName"/> or empty if <paramref name="valueName"/> does not exist</returns>

        public static string GetSetting(string baseKey, string appNameSubKey, string valueName)
        {
            string resultString = "";
            try
            {
                var registryKey = Registry.CurrentUser.OpenSubKey(string.Concat(baseKey, appNameSubKey), RegistryKeyPermissionCheck.ReadSubTree);
                if (registryKey != null)
                {
                    resultString = registryKey.GetValue(valueName)?.ToString();
                    registryKey.Close();
                }
            }
            catch (NullReferenceException)
            {
                return resultString;
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.GetSetting : Exception" + Environment.NewLine + "", exception.Message));
                throw;
            }

            return resultString;
        }

        /// <summary>This method gets <paramref name="valueName"/> as a string from the current user registry located in HKLM\<paramref name="baseKey"/>\<paramref name="appNameSubKey"/>\<paramref name="valueName"/></summary>
        /// <param name="baseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKLM</param>
        /// <param name="appNameSubKey">Name of the SubKey that <paramref name="valueName"/> is located in under <paramref name="baseKey"/></param>
        /// <param name="valueName">Name of value to retrieve</param>
        /// <returns>The string value from <paramref name="valueName"/> or empty if <paramref name="valueName"/> does not exist</returns>
        public static string GetSettingLm(string baseKey, string appNameSubKey, string valueName)
        {
            string resultString = "";
            try
            {
                var registryKey = Registry.LocalMachine.OpenSubKey(string.Concat(baseKey, appNameSubKey), RegistryKeyPermissionCheck.ReadSubTree);
                if (registryKey != null)
                {
                    resultString = registryKey.GetValue(valueName)?.ToString();
                    registryKey.Close();
                }
            }
            catch (NullReferenceException)
            {
                return resultString;
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.GetSettingLM : Exception" + Environment.NewLine + "", exception.Message));
                throw;
            }

            return resultString;
        }

        /// <summary>This method gets <paramref name="valueName"/> as an integer from the current user registry located in HKCU\<paramref name="baseKey"/>\<paramref name="appNameSubKey"/>\<paramref name="valueName"/></summary>
        /// <param name="baseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKCU</param>
        /// <param name="appNameSubKey">Name of the SubKey that <paramref name="valueName"/> is located in under <paramref name="baseKey"/></param>
        /// <param name="valueName">Name of value to retrieve</param>
        /// <returns>The integer value from <paramref name="valueName"/> or 0 if <paramref name="valueName"/> does not exist</returns>
        public static int GetInteger(string baseKey, string appNameSubKey, string valueName)
        {
            int resultInteger = 0;
            try
            {
                var registryKey = Registry.CurrentUser.OpenSubKey(string.Concat(baseKey, appNameSubKey), RegistryKeyPermissionCheck.ReadSubTree);
                if (registryKey != null)
                {
                        var rawResult = registryKey.GetValue(valueName);
                        if (rawResult != null)
                        {
                            RegistryValueKind resultKind = registryKey.GetValueKind(valueName);
                            switch (resultKind)
                            {
                                case RegistryValueKind.DWord:
                                    resultInteger = (int)rawResult;
                                    break;
                                case RegistryValueKind.QWord:
                                    resultInteger = (int)rawResult;
                                    break;
                                case RegistryValueKind.String:
                                    resultInteger = int.Parse((string)rawResult);
                                    break;
                            }
                        } 
                        registryKey.Close();
                }
            }
            catch (NullReferenceException)
            {
                return resultInteger;
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.GetInteger: Exception" + Environment.NewLine + "", exception.Message));
                throw;
            }

            return resultInteger;
        }

        /// <summary>This method gets <paramref name="valueName"/> as an integer from the local machine registry located in HKLM\<paramref name="baseKey"/>\<paramref name="appNameSubKey"/>\<paramref name="valueName"/></summary>
        /// <param name="baseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKLM</param>
        /// <param name="appNameSubKey">Name of the SubKey that <paramref name="valueName"/> is located in under <paramref name="baseKey"/></param>
        /// <param name="valueName">Name of value to retrieve</param>
        /// <returns>The integer value from <paramref name="valueName"/> or 0 if <paramref name="valueName"/> does not exist</returns>
        public static int GetIntegerLm(string baseKey,string appNameSubKey, string valueName)
        {
            int resultInteger = 0;
            try
            {
                var registryKey = Registry.LocalMachine.OpenSubKey(string.Concat(baseKey, appNameSubKey), RegistryKeyPermissionCheck.ReadSubTree);
                if (registryKey != null)
                {
                    var rawResult = registryKey.GetValue(valueName);
                    if (rawResult != null)
                    {
                        RegistryValueKind resultKind = registryKey.GetValueKind(valueName);
                        switch (resultKind)
                        {

                            case RegistryValueKind.DWord:
                                resultInteger = (int)rawResult;
                                break;
                            case RegistryValueKind.QWord:
                                resultInteger = (int)rawResult;
                                break;
                            case RegistryValueKind.String:
                                resultInteger = int.Parse((string)rawResult);
                                break;
                        }
                    }
                    registryKey.Close();
                }
            }
            catch (NullReferenceException)
            {
                return resultInteger;
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.GetIntegerLM: Exception" + Environment.NewLine + "", exception.Message));
                throw;
            }

            return resultInteger;
        }

        /// <summary>This method saves <paramref name="valueName"/> as a boolean to the current user registry located in HKCU\<paramref name="baseKey"/>\<paramref name="appNameSubKey"/>\<paramref name="valueName"/></summary>
        /// <param name="baseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKCU</param>
        /// <param name="appNameSubKey">Name of the SubKey that <paramref name="valueName"/> is located in under <paramref name="baseKey"/></param>
        /// <param name="valueName">Name of value</param>
        /// <param name="value">Value to be saved as boolean</param>
        public static void SaveBooleanSetting(string baseKey, string appNameSubKey, string valueName, bool value)
        {
            try
            {
                string combinedKey = string.Concat(baseKey, appNameSubKey);
                var registryKey = Registry.CurrentUser.CreateSubKey(combinedKey);
                if (registryKey != null)
                {
                    registryKey.SetValue(valueName, value);
                    registryKey.Close();
                }
                else
                {
                    throw new UnauthorizedAccessException("Unable to save");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.SaveBooleanSetting : Exception" + Environment.NewLine + "", exception.Message));
                throw;
            }
        }


        /// <summary>This method saves <paramref name="valueName"/> as a boolean to the local machine registry located in HKLM\<paramref name="baseKey"/>\<paramref name="appNameSubKey"/>\<paramref name="valueName"/></summary>
        /// <param name="baseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKLM</param>
        /// <param name="appNameSubKey">Name of the SubKey that <paramref name="valueName"/> is located in under <paramref name="baseKey"/></param>
        /// <param name="valueName">Name of value</param>
        /// <param name="value">Value to be saved as boolean</param>
        public static void SaveBooleanSettingLm(string baseKey, string appNameSubKey, string valueName, bool value)
        {
            try
            {
                string combinedKey = string.Concat(baseKey, appNameSubKey);
                var registryKey = Registry.LocalMachine.CreateSubKey(combinedKey);
                if (registryKey != null)
                {
                    registryKey.SetValue(valueName, value);
                    registryKey.Close();
                }
                else
                {
                    throw new UnauthorizedAccessException("Unable to save");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.SaveBooleanSettingLM : Exception" + Environment.NewLine + "", exception.Message));
                throw;
            }
        }

        /// <summary>This method saves <paramref name="valueName"/> as a string to the current user registry located in HKCU\<paramref name="baseKey"/>\<paramref name="appNameSubKey"/>\<paramref name="valueName"/></summary>
        /// <param name="baseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKCU</param>
        /// <param name="appNameSubKey">Name of the SubKey that <paramref name="valueName"/> is located in under <paramref name="baseKey"/></param>
        /// <param name="valueName">Name of value</param>
        /// <param name="value">Value to be saved as string</param>
        public static void SaveSetting(string baseKey, string appNameSubKey, string valueName, string value)
        {
            try
            {
                string combinedKey = string.Concat(baseKey, appNameSubKey);
                var registryKey = Registry.CurrentUser.CreateSubKey(combinedKey);
                if (registryKey != null)
                {
                    registryKey.SetValue(valueName, value, RegistryValueKind.String);
                    registryKey.Close();
                }
                else
                {
                    throw new UnauthorizedAccessException("Unable to save");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.SaveSetting : Exception" + Environment.NewLine + "", exception.Message));
                throw;
            }
        }

        /// <summary>This method saves <paramref name="valueName"/> as a string to the local machine registry located in HKLM\<paramref name="baseKey"/>\<paramref name="appNameSubKey"/>\<paramref name="valueName"/></summary>
        /// <param name="baseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKLM</param>
        /// <param name="appNameSubKey">Name of the SubKey that <paramref name="valueName"/> is located in under <paramref name="baseKey"/></param>
        /// <param name="valueName">Name of value</param>
        /// <param name="value">Value to be saved as string</param>
        public static void SaveSettingLm(string baseKey, string appNameSubKey, string valueName, string value)
        {
            try
            {
                string combinedKey = string.Concat(baseKey, appNameSubKey);
                var registryKey = Registry.LocalMachine.CreateSubKey(combinedKey);
                if (registryKey != null)
                {
                    registryKey.SetValue(valueName, value, RegistryValueKind.String);
                    registryKey.Close();
                }
                else
                {
                    throw new UnauthorizedAccessException("Unable to save");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.SaveSetting : Exception" + Environment.NewLine + "", exception.Message));
                throw;
            }
        }

        /// <summary>This method saves <paramref name="valueName"/> as a integer to the current user registry located in HKCU\<paramref name="baseKey"/>\<paramref name="appNameSubKey"/>\<paramref name="valueName"/></summary>
        /// <param name="baseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKCU</param>
        /// <param name="appNameSubKey">Name of the SubKey that <paramref name="valueName"/> is located in under <paramref name="baseKey"/></param>
        /// <param name="valueName">Name of value</param>
        /// <param name="value">Value to be saved as integer</param>
        public static void SaveInteger(string baseKey, string appNameSubKey, string valueName, int value)
        {
            try
            {
                string combinedKey = string.Concat(baseKey, appNameSubKey);
                var registryKey = Registry.CurrentUser.CreateSubKey(combinedKey);
                if (registryKey != null)
                {
                    registryKey.SetValue(valueName, value, RegistryValueKind.DWord);
                    registryKey.Close();
                }
                else
                {
                    throw new UnauthorizedAccessException("Unable to save");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.SaveInteger : Exception" + Environment.NewLine + "", exception.Message));
                throw ;
            }
        }

        /// <summary>This method saves <paramref name="valueName"/> as a integer to the local machine registry located in HKLM\<paramref name="baseKey"/>\<paramref name="appNameSubKey"/>\<paramref name="valueName"/></summary>
        /// <param name="baseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKLM</param>
        /// <param name="appNameSubKey">Name of the SubKey that <paramref name="valueName"/> is located in under <paramref name="baseKey"/></param>
        /// <param name="valueName">Name of value</param>
        /// <param name="value">Value to be saved as integer</param>
        public static void SaveIntegerLm(string baseKey, string appNameSubKey, string valueName, int value)
        {
            try
            {
                string combinedKey = string.Concat(baseKey, appNameSubKey);
                var registryKey = Registry.LocalMachine.CreateSubKey(combinedKey);
                if (registryKey != null)
                {
                    registryKey.SetValue(valueName, value, RegistryValueKind.DWord);
                    registryKey.Close();
                }
                else
                {
                    throw new UnauthorizedAccessException("Unable to save");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.SaveIntegerLM : Exception" + Environment.NewLine + "", exception.Message));
                throw;
            }
        }

        /// <summary>This method returns all value names within the local machine registry located in HKCU\<paramref name="baseKey"/>\<paramref name="appNameSubKey"/></summary>
        /// <param name="baseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKCU</param>
        /// <param name="appNameSubKey">Name of the SubKey to return all value names is located in under <paramref name="baseKey"/></param>
        public static string[] GetValueNames(string baseKey, string appNameSubKey)
        {
            string combinedKey = string.Concat(baseKey, appNameSubKey);
            var registryKey = Registry.CurrentUser.OpenSubKey(combinedKey);
            return registryKey != null ? registryKey.GetValueNames() : [];
        }

        /// <summary>This method returns all value names within the local machine registry located in HKLM\<paramref name="baseKey"/>\<paramref name="appNameSubKey"/></summary>
        /// <param name="baseKey">Base sub key and should look like SOFTWARE\MeaModGroup\ and will be located in HKLM</param>
        /// <param name="appNameSubKey">Name of the SubKey to return all value names is located in under <paramref name="baseKey"/></param>
        public static string[] GetValueNamesLM(string baseKey, string appNameSubKey)
        {
            string combinedKey = string.Concat(baseKey, appNameSubKey);
            var registryKey = Registry.LocalMachine.OpenSubKey(combinedKey);
            return registryKey != null ? registryKey.GetValueNames() : [];
        }
    }
}
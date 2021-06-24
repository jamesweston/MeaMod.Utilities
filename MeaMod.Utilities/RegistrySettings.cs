using Microsoft.Win32;
using System;

namespace MeaMod.Utilities
{
    public class RegistrySettings
    {
        /// <summary>This method Deletes <paramref name="Name"/>  from to the current user registry located in <paramref name="Base"/> / <paramref name="AppName"/></summary>
        /// <param name="AppName">Name of the SubKey that <param name="Name"> is located in under <paramref name="Base"/> SubKey</param>
        /// <param name="Name">Name of registry value to delete</param>
        /// <param name="Base">Base SubKey and should look like SOFTWARE\MeaModGroup\</param>
        public static void DeleteSetting(string AppName, string Name, string Base)
        {
            try
            {
                var registryKey = Registry.CurrentUser.CreateSubKey(string.Concat(Base, AppName));
                registryKey.DeleteValue(Name);
                registryKey.Close();
            }
            catch (NullReferenceException)
            {
            }
            // Already removed
            catch (Exception exception1)
            {
                var exception = exception1;
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.DeleteSetting : Exception" + System.Environment.NewLine + "", exception.Message));
                throw exception;
            }
        }

        /// <summary>This method Deletes <paramref name="Name"/>  from to the local machine registry located in <paramref name="Base"/> / <paramref name="AppName"/></summary>
        /// <param name="AppName">Name of the SubKey that <param name="Name"> is located in under <paramref name="Base"/> SubKey</param>
        /// <param name="Name">Name of registry value to delete</param>
        /// <param name="Base">Base SubKey and should look like SOFTWARE\MeaModGroup\</param>
        public static void DeleteSettingLM(string AppName, string Name, string Base)
        {
            try
            {
                var registryKey = Registry.LocalMachine.CreateSubKey(string.Concat(Base, AppName));
                registryKey.DeleteValue(Name);
                registryKey.Close();
            }
            catch (NullReferenceException)
            {
            }
            // Already removed
            catch (Exception exception1)
            {
                var exception = exception1;
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.DeleteSetting : Exception" + System.Environment.NewLine + "", exception.Message));
                throw exception;
            }
        }

        /// <summary>This method get <paramref name="Name"/> as a boolean from to the current user registry located in <paramref name="Base"/> / <paramref name="AppName"/></summary>
        /// <param name="AppName">Name of the SubKey that <param name="Name"> is located in under <paramref name="Base"/> SubKey</param>
        /// <param name="Name">Name of registry value to get</param>
        /// <param name="Base">Base SubKey and should look like SOFTWARE\MeaModGroup\</param>
        /// <param name="Default">Default value is <param name="Name"> does not exist</param>
        public static bool GetBooleanSetting(string AppName, string Name, string Base, bool Default = false)
        {
            bool flag = Default;
            try
            {
                var registryKey = Registry.CurrentUser.OpenSubKey(string.Concat(Base, AppName), RegistryKeyPermissionCheck.ReadSubTree);
                flag = Convert.ToBoolean(registryKey.GetValue(Name));
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

        /// <summary>This method get <paramref name="Name"/> as a boolean from to the local machine registry located in <paramref name="Base"/> / <paramref name="AppName"/></summary>
        /// <param name="AppName">Name of the SubKey that <param name="Name"> is located in under <paramref name="Base"/> SubKey</param>
        /// <param name="Name">Name of registry value to get</param>
        /// <param name="Base">Base SubKey and should look like SOFTWARE\MeaModGroup\</param>
        /// <param name="Default">Default value is <param name="Name"> does not exist</param>
        public static bool GetBooleanSettingLM(string AppName, string Name, string Base, bool Default = false)
        {
            bool flag = Default;
            try
            {
                var registryKey = Registry.LocalMachine.OpenSubKey(string.Concat(Base, AppName), RegistryKeyPermissionCheck.ReadSubTree);
                flag = Convert.ToBoolean(registryKey.GetValue(Name));
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

        public static string GetSetting(string AppName, string Name, string Base)
        {
            string str = "";
            try
            {
                var registryKey = Registry.CurrentUser.OpenSubKey(string.Concat(Base, AppName), RegistryKeyPermissionCheck.ReadSubTree);
                str = registryKey.GetValue(Name).ToString();
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

        public static string GetSettingLM(string AppName, string Name, string Base)
        {
            string str = "";
            try
            {
                var registryKey = Registry.LocalMachine.OpenSubKey(string.Concat(Base, AppName), RegistryKeyPermissionCheck.ReadSubTree);
                str = registryKey.GetValue(Name).ToString();
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

        public static int GetInteger(string AppName, string Name, string Base)
        {
            int inti = 0;
            try
            {
                var registryKey = Registry.CurrentUser.OpenSubKey(string.Concat(Base, AppName), RegistryKeyPermissionCheck.ReadSubTree);
                inti = int.Parse((string)registryKey.GetValue(Name));
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

        public static int GetIntegerLM(string AppName, string Name, string Base)
        {
            int inti = 0;
            try
            {
                var registryKey = Registry.LocalMachine.OpenSubKey(string.Concat(Base, AppName), RegistryKeyPermissionCheck.ReadSubTree);
                inti = int.Parse((string)registryKey.GetValue(Name));
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

        // ----- Save Entry -----

        public static void SaveBooleanSetting(string AppName, string Name, bool Value, string Base)
        {
            try
            {
                string str = string.Concat(Base, AppName);
                var registryKey = Registry.CurrentUser.CreateSubKey(str);
                registryKey.SetValue(Name, Value);
                registryKey.Close();
            }
            catch (Exception exception1)
            {
                var exception = exception1;
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.SaveBooleanSetting : Exception" + System.Environment.NewLine + "", exception.Message));
                throw exception;
            }
        }

        public static void SaveBooleanSettingLM(string AppName, string Name, bool Value, string Base)
        {
            try
            {
                string str = string.Concat(Base, AppName);
                var registryKey = Registry.LocalMachine.CreateSubKey(str);
                registryKey.SetValue(Name, Value);
                registryKey.Close();
            }
            catch (Exception exception1)
            {
                var exception = exception1;
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.SaveBooleanSettingLM : Exception" + System.Environment.NewLine + "", exception.Message));
                throw exception;
            }
        }

        public static void SaveSetting(string AppName, string Name, string Value, string Base)
        {
            try
            {
                string str = string.Concat(Base, AppName);
                var registryKey = Registry.CurrentUser.CreateSubKey(str);
                registryKey.SetValue(Name, Value, RegistryValueKind.String);
                registryKey.Close();
            }
            catch (Exception exception1)
            {
                var exception = exception1;
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.SaveSetting : Exception" + System.Environment.NewLine + "", exception.Message));
                throw exception;
            }
        }

        public static void SaveSettingLM(string AppName, string Name, string Value, string Base)
        {
            try
            {
                string str = string.Concat(Base, AppName);
                var registryKey = Registry.LocalMachine.CreateSubKey(str);
                registryKey.SetValue(Name, Value, RegistryValueKind.String);
                registryKey.Close();
            }
            catch (Exception exception1)
            {
                var exception = exception1;
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.SaveSetting : Exception" + System.Environment.NewLine + "", exception.Message));
                throw exception;
            }
        }

        public static void SaveInteger(string AppName, string Name, int Value, string Base)
        {
            try
            {
                string str = string.Concat(Base, AppName);
                var registryKey = Registry.CurrentUser.CreateSubKey(str);
                registryKey.SetValue(Name, Value, RegistryValueKind.DWord);
                registryKey.Close();
            }
            catch (Exception exception1)
            {
                var exception = exception1;
                Console.WriteLine(string.Concat("MeaMod.Utilities.RegistrySettings.SaveInteger : Exception" + System.Environment.NewLine + "", exception.Message));
                throw exception;
            }
        }

        public static void SaveIntegerLM(string AppName, string Name, int Value, string Base)
        {
            try
            {
                string str = string.Concat(Base, AppName);
                var registryKey = Registry.LocalMachine.CreateSubKey(str);
                registryKey.SetValue(Name, Value, RegistryValueKind.DWord);
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
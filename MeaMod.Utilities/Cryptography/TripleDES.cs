using System;

namespace MeaMod.Utilities.Cryptography
{
    public class TripleDES
    {
        public static string EncryptTripleDES(string sIn, string sKey)
        {
            var DES = new System.Security.Cryptography.TripleDESCryptoServiceProvider();
            var hashMD5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            // scramble the key
            sKey = ScrambleKey(sKey);
            // Compute the MD5 hash.
            DES.Key = hashMD5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(sKey));
            // Set the cipher mode.
            DES.Mode = System.Security.Cryptography.CipherMode.ECB;
            // Create the encryptor.
            var DESEncrypt = DES.CreateEncryptor();
            // Get a byte array of the string.
            var Buffer = System.Text.Encoding.ASCII.GetBytes(sIn);
            // Transform and return the string.
            return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

        public static string DecryptTripleDES(string sOut, string sKey)
        {
            var DES = new System.Security.Cryptography.TripleDESCryptoServiceProvider();
            var hashMD5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            // scramble the key
            sKey = ScrambleKey(sKey);
            // Compute the MD5 hash.
            DES.Key = hashMD5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(sKey));
            // Set the cipher mode.
            DES.Mode = System.Security.Cryptography.CipherMode.ECB;
            // Create the decryptor.
            var DESDecrypt = DES.CreateDecryptor();
            var Buffer = Convert.FromBase64String(sOut);
            // Transform and return the string.
            return System.Text.Encoding.ASCII.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

        public static string ScrambleKey(string v_strKey)
        {
            var sbKey = new System.Text.StringBuilder();
            int intPtr;
            var loopTo = v_strKey.Length;
            for (intPtr = 1; intPtr <= loopTo; intPtr++)
            {
                int intIn = v_strKey.Length - intPtr + 1;
                sbKey.Append(v_strKey.Substring(intIn, 1));
            }
            return sbKey.ToString();
        }
    }
}

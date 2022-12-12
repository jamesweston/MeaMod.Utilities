using System;
using System.Text;

namespace MeaMod.Utilities.Cryptography
{
    [Obsolete("Hash is deprecated, please use System.Security.Cryptography instead.")]
    public class Hash
    {
        [Obsolete("MD5 is deprecated, please use System.Security.Cryptography.MD5 instead.")]
        public static string MD5(string strToEncrypt)
        {
            var ue = new UTF8Encoding();
            var bytes = ue.GetBytes(strToEncrypt);
            var l_md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var hashBytes = l_md5.ComputeHash(bytes);
            string hashString = "";
            int i = 0;
            while (i < hashBytes.Length)
            {
                hashString += Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
                Math.Min(System.Threading.Interlocked.Increment(ref i), i - 1);
            }

            return hashString.PadLeft(32, '0');
        }

        [Obsolete("SHA1 is deprecated, please use System.Security.Cryptography.SHA1 instead.")]
        public static string SHA1(string strToEncrypt)
        {
            var ue = new UTF8Encoding();
            var bytes = ue.GetBytes(strToEncrypt);
            var l_sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            var hashBytes = l_sha1.ComputeHash(bytes);
            string hashString = "";
            int i = 0;
            while (i < hashBytes.Length)
            {
                hashString += Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
                Math.Min(System.Threading.Interlocked.Increment(ref i), i - 1);
            }

            return hashString.PadLeft(32, '0');
        }

        [Obsolete("SHA265 is deprecated, please use System.Security.Cryptography.SHA265 instead.")]
        public static string SHA256(string strToEncrypt)
        {
            var ue = new UTF8Encoding();
            var bytes = ue.GetBytes(strToEncrypt);
            System.Security.Cryptography.SHA256 l_sha256 = new System.Security.Cryptography.SHA256Managed();
            var hashBytes = l_sha256.ComputeHash(bytes);
            string hashString = "";
            int i = 0;
            while (i < hashBytes.Length)
            {
                hashString += Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
                Math.Min(System.Threading.Interlocked.Increment(ref i), i - 1);
            }

            return hashString.PadLeft(32, '0');
        }

        [Obsolete("SHA384 is deprecated, please use System.Security.Cryptography.SHA384 instead.")]
        public static string SHA384(string strToEncrypt)
        {
            var ue = new UTF8Encoding();
            var bytes = ue.GetBytes(strToEncrypt);
            System.Security.Cryptography.SHA384 l_sha384 = new System.Security.Cryptography.SHA384Managed();
            var hashBytes = l_sha384.ComputeHash(bytes);
            string hashString = "";
            int i = 0;
            while (i < hashBytes.Length)
            {
                hashString += Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
                Math.Min(System.Threading.Interlocked.Increment(ref i), i - 1);
            }

            return hashString.PadLeft(32, '0');
        }

        [Obsolete("SHA512 is deprecated, please use System.Security.Cryptography.SHA512 instead.")]
        public static string SHA512(string strToEncrypt)
        {
            var ue = new UTF8Encoding();
            var bytes = ue.GetBytes(strToEncrypt);
            System.Security.Cryptography.SHA512 l_sha512 = new System.Security.Cryptography.SHA512Managed();
            var hashBytes = l_sha512.ComputeHash(bytes);
            string hashString = "";
            int i = 0;
            while (i < hashBytes.Length)
            {
                hashString += Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
                Math.Min(System.Threading.Interlocked.Increment(ref i), i - 1);
            }

            return hashString.PadLeft(32, '0');
        }
    }
}

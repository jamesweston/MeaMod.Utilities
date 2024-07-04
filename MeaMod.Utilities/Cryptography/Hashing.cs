using System;
using System.Security.Cryptography;
using System.Text;

namespace MeaMod.Utilities.Cryptography
{
    /// <summary>
    /// Cryptography Hashing Class
    /// </summary>
    public class Hashing
    {
        /// <summary>
        /// Computes the hash value for the input string.
        /// <para><see href="https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.hashalgorithm.computehash"/></para>
        /// <para>Licence: CC BY 4.0</para>
        /// </summary>
        /// <param name="hashAlgorithm">Specifies cryptographic hash algorithm.</param>
        /// <param name="input">Input string to be hashed</param>
        /// <returns>String of hash</returns>
        public static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            foreach (byte t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        /// <summary>
        /// Computes the SHA256 hash value for the input string.
        /// </summary>
        /// <param name="input">Input string to be hashed</param>
        /// <returns>SHA256 hash of input</returns>
        public static string GetHashSHA256(string input)
        {
            using var hash = SHA256.Create();
            return GetHash(hash, input);
        }

        /// <summary>
        /// Computes the SHA384 hash value for the input string.
        /// </summary>
        /// <param name="input">Input string to be hashed</param>
        /// <returns>SHA384 hash of input</returns>
        public static string GetHashSHA384(string input)
        {
            using var hash = SHA384.Create();
            return GetHash(hash, input);
        }

        /// <summary>
        /// Computes the SHA512 hash value for the input string.
        /// </summary>
        /// <param name="input">Input string to be hashed</param>
        /// <returns>SHA512 hash of input</returns>
        public static string GetHashSHA512(string input)
        {
            using var hash = SHA512.Create();
            return GetHash(hash, input);
        }
    }
}

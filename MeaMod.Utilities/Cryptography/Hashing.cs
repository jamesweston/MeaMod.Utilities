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
        /// <returns>Strong of hash</returns>
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
    }
}

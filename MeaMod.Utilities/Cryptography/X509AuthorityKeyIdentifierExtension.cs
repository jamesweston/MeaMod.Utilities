using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Diagnostics.Contracts;

namespace MeaMod.Utilities.Cryptography
{
    /// <summary>
    /// Add X509AuthorityKeyIdentifierExtension to .net standard 2.0
    /// <para><see href="https://shawinnes.com/dotnet-x509-extensions/"/></para>
    /// </summary>
    public class X509AuthorityKeyIdentifierExtension : X509Extension
    {
        private static Oid AuthorityKeyIdentifierOid => new Oid("2.5.29.35");
        private static Oid SubjectKeyIdentifierOid => new Oid("2.5.29.14");

        /// <summary>
        /// Initializes a new instance of the X509AuthorityKeyIdentifierExtension class using a X509Certificate2 and a value indicating whether the extension is critical.
        /// </summary>
        /// <param name="certificateAuthority">X509Certificate2</param>
        /// <param name="critical">Boolean</param>
        public X509AuthorityKeyIdentifierExtension(X509Certificate2 certificateAuthority, bool critical)
            : base(AuthorityKeyIdentifierOid, EncodeExtension(certificateAuthority), critical)
        {
        }

        private static byte[] EncodeExtension(X509Certificate2 certificateAuthority)
        {
            var subjectKeyIdentifier = certificateAuthority.Extensions.Cast<X509Extension>().FirstOrDefault(p => p.Oid?.Value == SubjectKeyIdentifierOid.Value);
            if (subjectKeyIdentifier == null)
                return null;
            var rawData = subjectKeyIdentifier.RawData;
            var segment = new ArraySegment<byte>(rawData, 2, rawData.Length - 2);
            var authorityKeyIdentifier = new byte[segment.Count + 4];
            // KeyID of the AuthorityKeyIdentifier
            authorityKeyIdentifier[0] = 0x30;
            authorityKeyIdentifier[1] = 0x16;
            authorityKeyIdentifier[2] = 0x80;
            authorityKeyIdentifier[3] = 0x14;
            //segment.CopyTo(authorityKeyIdentifier, 4);
            Array.Copy(segment.Array,0,authorityKeyIdentifier,4,segment.Count);
            return authorityKeyIdentifier;
        }
    }
}

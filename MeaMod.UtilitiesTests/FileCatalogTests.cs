using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Diagnostics;
using MeaMod.Utilities.Security;

namespace MeaMod.Utilities.Tests
{
    [TestClass()]
    public class FileCatalogTests
    {
        private static readonly string Path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "","testfiles");
        private static readonly string CatalogFolder = System.IO.Path.Combine(Path, "vlc");
        private static readonly Collection<string> Paths =
        [
            CatalogFolder
        ];

        [TestMethod()]
        public void ValidateTestValidUnsigned()
        {
            var catalogFile = System.IO.Path.Combine(Path, "vlc_valid.cat");

            CatalogInformation catalogInfo = FileCatalog.Validate(Paths, catalogFile);
            Assert.IsNotNull(catalogInfo);
            Assert.IsTrue(catalogInfo.Signature.Status == SignatureStatus.NotSigned);
            Assert.IsTrue(catalogInfo.Status == CatalogValidationStatus.Valid);
        }
        [TestMethod()]
        public void ValidateTestValidSigned()
        {
            var catalogFile = System.IO.Path.Combine(Path, "vlc_valid_signed.cat");
            CatalogInformation catalogInfo = FileCatalog.Validate(Paths, catalogFile);
            Assert.IsNotNull(catalogInfo);
            Assert.IsTrue(catalogInfo.Signature.Status == SignatureStatus.Valid);
            Assert.IsTrue(catalogInfo.Status == CatalogValidationStatus.Valid);
        }

        [TestMethod()]
        public void ValidateTestInvalidSigned()
        {
            var catalogFile = System.IO.Path.Combine(Path, "vlc_invalid_signed.cat");
            CatalogInformation catalogInfo = FileCatalog.Validate(Paths, catalogFile);
            Assert.IsNotNull(catalogInfo);
            Assert.IsTrue(catalogInfo.Signature.Status == SignatureStatus.Valid);
            Assert.IsTrue(catalogInfo.Status == CatalogValidationStatus.ValidationFailed);
        }

        [TestMethod()]
        public void ValidateTestInvalidSignedInvalid()
        {
            var catalogFile = System.IO.Path.Combine(Path, "vlc_invalid_signed_invalid.cat");
            CatalogInformation catalogInfo = FileCatalog.Validate(Paths, catalogFile);
            Assert.IsNotNull(catalogInfo);
            Assert.IsTrue(catalogInfo.Signature.Status == SignatureStatus.NotSigned);
            Assert.IsTrue(catalogInfo.Status == CatalogValidationStatus.ValidationFailed);
        }

        [TestMethod()]
        public void ValidateTestInvalidUnsigned()
        {
            var catalogFile = System.IO.Path.Combine(Path, "vlc_invalid.cat");
            CatalogInformation catalogInfo = FileCatalog.Validate(Paths, catalogFile);
            Assert.IsNotNull(catalogInfo);
            Assert.IsTrue(catalogInfo.Signature.Status == SignatureStatus.NotSigned);
            Assert.IsTrue(catalogInfo.Status == CatalogValidationStatus.ValidationFailed);
        }
    }
}
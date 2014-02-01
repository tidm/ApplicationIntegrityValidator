using System;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApplicationIntegrityValidator.Test
{
    [TestClass]
    public class FolderIntegrityValidatorTests
    {
        [TestMethod]
        public void FolderExistsMustReturnPassedResultInCaseOfAnExistingFolder()
        {
            var tester = new IntegrityValidator();
            const string path = @"c:\windows\";
            var result = tester.Folder(path).Exists();

            Assert.AreEqual("Ensure Folder c:\\windows exists", result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result, typeof(FolderIntegrityValidator));
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
        }

        [TestMethod]
        public void FolderExistsMustReturnFaildResultInCaseOfAnNonExistingFolder()
        {
            var tester = new IntegrityValidator();
            const string path = @"c:\windows1\";
            var result = tester.Folder(path).Exists();

            Assert.AreEqual("Ensure Folder c:\\windows1 exists", result.First().Description);
            Assert.IsFalse(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result, typeof(FolderIntegrityValidator));
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
        }

        [TestMethod]
        public void FolderMustHaveAttributes()
        {
            var tester = new IntegrityValidator();
            const string path = @"c:\IntegTest\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var folder = new DirectoryInfo(path);
            folder.Attributes = FileAttributes.Offline;
            var result = tester.Folder(path).HasAttributes(FileAttributes.Offline);

            Assert.AreEqual("Ensure Folder c:\\IntegTest has Offline attribute(s)", result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
        }

        [TestMethod]
        public void FolderChainingTest()
        {
            var tester = new IntegrityValidator();
            const string path = @"c:\IntegTest\";
            if (Directory.Exists(path))
                Directory.CreateDirectory(path);
            var folder = new DirectoryInfo(path);
            folder.Attributes = FileAttributes.Offline | FileAttributes.Hidden;
            var result = tester.Folder(path).Exists().HasAttributes(FileAttributes.Offline | FileAttributes.Hidden);

            Assert.AreEqual("Ensure Folder c:\\IntegTest exists", result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);


            Assert.AreEqual("Ensure Folder c:\\IntegTest has Hidden, Offline attribute(s)", result.Last().Description);
            Assert.IsTrue(result.Last().Succeed);
            Assert.IsNull(result.Last().Exception);

            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
            Assert.IsInstanceOfType(result.Last(), typeof(IntegrityValidationResult));

            Assert.IsInstanceOfType(result, typeof(FolderIntegrityValidator));


        }
    }
}

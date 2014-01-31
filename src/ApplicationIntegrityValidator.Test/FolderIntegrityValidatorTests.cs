using System;
using System.IO;
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
            const string path = @"c:\windows";
            var result = tester.Folder(path).Exists();

            Assert.AreEqual("Ensure Folder windows exists in c:\\", result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result, typeof(FolderIntegrityValidator));
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
        }

        public void FolderExistsMustReturnFaildResultInCaseOfAnNonExistingFolder()
        {
            var tester = new IntegrityValidator();
            const string path = @"c:\windows1";
            var result = tester.Folder(path).Exists();

            Assert.AreEqual("Ensure Folder windows1 exists in c:\\", result.First().Description);
            Assert.IsFalse(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result, typeof(FolderIntegrityValidator));
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
        }

        public void FolderMustHaveAttributes()
        {
            var tester = new IntegrityValidator();
            const string path = @"c:\IntegTest";
            if (Directory.Exists(path))
                Directory.CreateDirectory(path);
            var folder = new DirectoryInfo(path);
            folder.Attributes = FileAttributes.Normal;
            var result = tester.Folder(path).HasAttributes(FileAttributes.Normal);

            Assert.AreEqual("Ensure Folder IntegTest has normal attribute.", result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
        }

        public void FileChainingTest()
        {
            var tester = new IntegrityValidator();
            const string path = @"c:\IntegTest";
            if (Directory.Exists(path))
                Directory.CreateDirectory(path);
            var folder = new DirectoryInfo(path);
            folder.Attributes = FileAttributes.Normal;
            var result = tester.Folder(path).Exists().HasAttributes(FileAttributes.Normal);

            Assert.AreEqual("Ensure Folder windows exists in c:\\", result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);


            Assert.AreEqual("Ensure Folder IntegTest has normal attribute.", result.First().Description);
            Assert.IsTrue(result.Last().Succeed);
            Assert.IsNull(result.Last().Exception);

            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
            Assert.IsInstanceOfType(result.Last(), typeof(IntegrityValidationResult));

            Assert.IsInstanceOfType(result, typeof(FolderIntegrityValidator));


        }
    }
}

using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApplicationIntegrityValidator.Test
{
    [TestClass]
    public class FileIntegrityValidatorTests
    {
        [TestMethod]
        public void FileExistsMustReturnPassedResultInCaseOfAnExistingFile()
        {
            var tester = new IntegrityValidator();
            var result = ((FileIntegrityValidator)tester.File(@"c:\windows\notepad.exe")).Exists();

            Assert.AreEqual("Ensure File notepad.exe exists in c:\\windows", ((IntegrityValidationResult)result.First()).Description);
            Assert.IsTrue(((IntegrityValidationResult)result.First()).Succeed);
            Assert.IsNull(((IntegrityValidationResult)result.First()).Exception);
            Assert.IsInstanceOfType(result, typeof(FileIntegrityValidator));
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
        }

        [TestMethod]
        public void FileExistsMustReturnFailedResultInCaseOfANonExistingFile()
        {
            var tester = new IntegrityValidator();
            var result = ((FileIntegrityValidator)tester.File(@"c:\windows\notepad1.exe")).Exists();

            Assert.AreEqual("Ensure File notepad1.exe exists in c:\\windows", ((IntegrityValidationResult)result.First()).Description);
            Assert.IsFalse(((IntegrityValidationResult)result.First()).Succeed);
            Assert.IsNull(((IntegrityValidationResult)result.First()).Exception);
            Assert.IsInstanceOfType(result, typeof(FileIntegrityValidator));
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
        }

        [TestMethod]
        public void FileMustHaveAttributes()
        {
            var tester = new IntegrityValidator();
            const string path = @"c:\IntegrityTest.txt";
            if (!File.Exists(path))
                File.Create(path);
            File.SetAttributes(path, FileAttributes.ReadOnly | FileAttributes.System);
            var result = tester.File(path).HasAttributes(FileAttributes.ReadOnly | FileAttributes.System);

            Assert.AreEqual("Ensure File IntegrityTest.txt has ReadOnly, System attributes", result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
        }

        [TestMethod]
        public void FileChainingTest()
        {
            var tester = new IntegrityValidator();
            const string path = @"c:\IntegrityTest.txt";
            if (!File.Exists(path))
                File.Create(path);
            File.SetAttributes(path, FileAttributes.ReadOnly | FileAttributes.System);
            var result = tester.File(path).Exists().HasAttributes(FileAttributes.ReadOnly | FileAttributes.System);

            Assert.AreEqual("Ensure File IntegrityTest.txt exists in c:\\", result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));

            Assert.AreEqual("Ensure File IntegrityTest.txt has ReadOnly, System attributes", result.Last().Description);
            Assert.IsTrue(result.Last().Succeed);
            Assert.IsNull(result.Last().Exception);
            Assert.IsInstanceOfType(result.Last(), typeof(IntegrityValidationResult));
        }

    }

    //public void test()
    //{
    //    IntegirytTester IntegirytTester = new IntegirytTester();
    //    //IntegirytTester.Database.Should.Contain.Table("User").Column("Id");
    //    //IntegritiyTester.FromConfig("ConfigFile");
    //    IntegirytTester.ConnectionString.Exists("connectionstring");
    //    IntegirytTester.AppSettings.Exists("appsetting");

    //    IntegrityTester.File.Exists("fileName");
    //    IntegrityTester.Folder.Exists("FolderName");

    //    var validationResult = IntegirytTester.Database("ConnectionString").Table("Tableame").Exists();

    //    validationResult.Task = "Ensure Table Tableame exists in Db with Connection String '/*ConnectionString*/'";
    //    validationResult.Result = ValidationResults.Passed;
    //    validationResult.Exception = null;

    //    var results = IntegirytTester.Database("ConnectionString").Table("Tableame").RowCount(rc => rc > 10).
    //    Database("ConnectionString").Table("Tablename").HasColumns("c1", "c2", "c3").
    //    IntegirytTester.Database("ConnectionString").Table("Tablename").Column("c1").Exists();
    //    IntegirytTester.Database("ConnectionString").Table("Tablename").Column("c1").TypeIs(typeof(int));
    //    IntegirytTester.Database("ConnectionString").Table("Tablename").Column("c1").HasIndex();
    //    IntegirytTester.Database("ConnectionString").Table("Tablename").Column("c1").IsPrimaryKey();
    //    IntegirytTester.Database("ConnectionString").Table("Tablename").Column("c1").IsForeignKey();
    //    IntegirytTester.Database("ConnectionString").Table("Tablename").Column("c1").IsNullable(true);
    //    IntegirytTester.Database("ConnectionString").Table("Tablename").Column(new Cdef());
    //    IntegirytTester.Database("ConnectionString").Table("Tablename", (Cols, rows) =>  );
    //    //IntegirytTester.Database("ConnectionString").Table("Tablename").HasRow(new {C1 = "c1", C2 = "c2"});
    //    //IntegirytTester.Database("ConnectionString").Table("Tableame").(rc => rc > 10);
    //    IntegirytTester.Database("ConnectionString").Sql("select * from User").Exists(); // IsValid
    //    IntegirytTester.Database("ConnectionString").Sql("select * from User Where userName == 'Administrator'").Result.Count(c => c == 1, "error message");
    //    IntegirytTester.Database("ConnectionString").Sql("select * from User Where userName == 'Administrator'").Result.Any(r => r.IsLocked == 1, "error message");

    //    IntegirytTester.Database("ConnectionString").StoredProcedure("Spname").Exists().Results;

    //    var database = IntegirytTester.Database("ConnectionString");
    //    var table = databse.Table("tn");
    //    var column = table.Column("cn");
    //    column.isType().Exists();
    //    table.Column().RowCount().HasColumns();


    //    IntegrityTester.FromAssembly(assembly).Run();


    //    var result = IntegrityTester.File.Exists("fileName")
    //                                .Folder.Exists("FolderName")
    //                                .Database("conn").Table("tbl").Exists()
    //                                                              .Column("c1").Exists()
    //                                                                           .TypeIs(typeof(int))
    //                                                 .Sql("").IsValid()
    //                                                              .Column("c2").Exists()
    //                                                 .Table("tbl2").Exists()
    //                                                 .Sql("select * from ...").IsValid()
    //                                .FromAssembly("asmFile").Run()
    //                                .FromAssembly("asm2").Run();

    //    var result = IntegrityTester.File.Exists("fileName")
    //                                .Database("conn").Table("tbl").Exists()
    //                                .Folder.Exists("FolderName")
    //                                                              .Column("c1").Exists()
    //                                                                           .TypeIs(typeof(int))
    //                                                 .Sql("").IsValid()
    //                                                              .Column("c2").Exists()
    //                                                 .Table("tbl2").Exists()
    //                                                 .Sql("select * from ...").IsValid()
    //                                .FromAssembly("asmFile").Run()
    //                                .FromAssembly("asm2").Run();

    //    var result = new List<ValidationResult>();
    //    var result = IntegirytTester.File("filename").Exists().IsReadOnly();
    //    var db1 = IntegirytTester.Database("dbConn");
    //    result.AddRange(db1.Table("tbl1").Exists().IsEmpty());
    //    result.AddRange(db1.Table("tbl1").Column("c1").Exists());



    //}

    //interface IFileIntegrityValidator : IEnumerable<ValidationResult>
    //{
    //    IFileIntegrityValidator Exists();
    //}


    //#region Folder
    //[TestMethod]
    //public void FolderExistsMustReturnPassedResultInCaseOfAnExistingFolder()
    //{
    //    IntegrityTester tester = new IntegrityTester();
    //    var result = tester.Folder.Exists(@"c:\windows\");

    //    Assert.AreEqual("Ensure Folder windows exists in c:\\", result.Result.First().Task);
    //    Assert.AreEqual(ValidationResults.Passed, result.Result.First().Result);
    //    Assert.IsNull(result.Result.First().Exception);
    //}

    //[TestMethod]
    //public void FolderExistsMustReturnFailedResultInCaseOfANonExistingFolder()
    //{
    //    IntegrityTester tester = new IntegrityTester();
    //    var result = tester.File.Exists(@"c:\windows\");

    //    Assert.AreEqual("Ensure Folder windows exists in c:\\", result.Result.First().Task);
    //    Assert.AreEqual(ValidationResults.Failed, result.Result.First().Result);
    //    Assert.IsNotNull(result.Result.First().Exception);
    //}
    //#endregion
}

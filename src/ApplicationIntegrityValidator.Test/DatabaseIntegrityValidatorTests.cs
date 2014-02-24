using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using Codeplex.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApplicationIntegrityValidator.Test
{
    [TestClass]
    public class DatabaseIntegrityValidatorTests
    {

        #region Table
        [TestMethod]
        public void SpecificTableOfDatabaseMustReturnPassedResultInCaseOfExistingTable()
        {
            var tester = new IntegrityValidator();

            var connectionString = ConfigurationManager.ConnectionStrings["connectionString1"].ConnectionString;

            var result = tester.Database(connectionString).Table("SEC_USER").Exists();

            Assert.AreEqual("Ensure Table: 'SEC_USER' exists in database", result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
            Assert.IsInstanceOfType(result, typeof(TableIntegrityValidator));
        }

        [TestMethod]
        public void SpecificTableOfDatabaseMustReturnFailedResultInCaseOfNonExistingTable()
        {
            var tester = new IntegrityValidator();

            var connectionString = ConfigurationManager.ConnectionStrings["connectionString1"].ConnectionString;

            var result = tester.Database(connectionString).Table("KharchangGhoorbaghe").Exists();

            Assert.AreEqual("Ensure Table: 'KharchangGhoorbaghe' exists in database", result.First().Description);
            Assert.IsFalse(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
            Assert.IsInstanceOfType(result, typeof(TableIntegrityValidator));
        }

        [TestMethod]
        public void SpecificTableOfDatabaseMustReturnPassedIfHasSpecificRangeOfColumns()
        {
            var tester = new IntegrityValidator();

            var connectionString = ConfigurationManager.ConnectionStrings["connectionString1"].ConnectionString;

            var columnsName = new List<string>() { "ID", "USERNAME", "PASSWORD", "EMAIL", "OTHERINFORMATIONS", "COMMENTS" };

            var result = tester.Database(connectionString).Table("SEC_USER").HasColumns(columnsName);
            var names = columnsName[0];
            for (int i = 1; i < columnsName.Count; i++)
            {
                names += ", " + columnsName[i];
            }

            Assert.AreEqual("Ensure Table: 'SEC_USER' has these columns: " + names, result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
            Assert.IsInstanceOfType(result, typeof(TableIntegrityValidator));
        }

        [TestMethod]
        public void SpecificTableOfDatabaseHasRowCount()
        {
            var tester = new IntegrityValidator();
            var result = tester.Database(ConfigurationManager.ConnectionStrings["connectionString1"].ConnectionString).Table("SEC_USER").RowCount(rc => rc > 10);

            Assert.AreEqual("Ensure Table: 'SEC_USER' has rows", result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
        }

        //public void SpecificTableOfDatabaseMustReturnRowCount()
        //{
        //    var tester = new IntegrityValidator();

        //    var connectionString = ConfigurationManager.ConnectionStrings["connectionString1"].ConnectionString;
        //    var rowCount = DbExecutor.ExecuteReader(
        //        new SqlConnection(connectionString), "SELECT * FROM  where table_name = 'SEC_USER'").Count();

        //    var result = tester.Database(connectionString).Table("SEC_USER").RowCount();

        //    Assert.AreEqual("Ensure Table: 'SEC_USER' exists in database by connectionstring: 'connectionString1'", result.First().Description);
        //    Assert.IsTrue(rowCount, result.First().Succeed);
        //    Assert.IsNull(result.First().Exception);
        //    Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
        //    Assert.IsInstanceOfType(result, typeof(DatabaseIntegrityValidator));
        //}

        #endregion

        #region Column

        [TestMethod]
        public void SpecificColumnOfTableMustReturnPassedResultInCaseOfExistingColumn()
        {
            var tester = new IntegrityValidator();

            var connectionString = ConfigurationManager.ConnectionStrings["connectionString1"].ConnectionString;

            var result = tester.Database(connectionString).Table("SEC_USER").Column("COMMENTS").Exists();

            Assert.AreEqual("Ensure Table: 'SEC_USER' has 'COMMENTS' column", result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
            Assert.IsInstanceOfType(result, typeof(ColumnIntegrityValidator));
        }

        [TestMethod]
        public void SpecificColumnOfTableMustReturnFailedResultInCaseOfNonExistingColumn()
        {
            var tester = new IntegrityValidator();

            var connectionString = ConfigurationManager.ConnectionStrings["connectionString1"].ConnectionString;

            var result = tester.Database(connectionString).Table("SEC_USER").Column("Nima").Exists();

            Assert.AreEqual("Ensure Table: 'SEC_USER' has 'Nima' column", result.First().Description);
            Assert.IsFalse(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
            Assert.IsInstanceOfType(result, typeof(ColumnIntegrityValidator));
        }

        [TestMethod]
        public void SpecificColumnOfTableMustHaveSpecificType()
        {
            var tester = new IntegrityValidator();

            var connectionString = ConfigurationManager.ConnectionStrings["connectionString1"].ConnectionString;

            var result = tester.Database(connectionString).Table("SEC_USER").Column("COMMENTS").TypeIs(OracleDataType.NVarchar2, 100);

            Assert.AreEqual(string.Format("Ensure Table: 'SEC_USER' has 'COMMENTS' column with '{0}' data type and size '{1}'", OracleDataType.NVarchar2, 100), result.First().Description);
            Assert.IsFalse(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
            Assert.IsInstanceOfType(result, typeof(ColumnIntegrityValidator));
        }

        [TestMethod]
        public void SpecificColumnOfTableMustHaveIndex()
        {
            var tester = new IntegrityValidator();

            var connectionString = ConfigurationManager.ConnectionStrings["connectionString1"].ConnectionString;

            var result = tester.Database(connectionString).Table("SEC_USER").Column("COMMENTS").HasIndex("USERNAME_UNIQUE");

            Assert.AreEqual("Ensure Table: 'SEC_USER' has 'COMMENTS' column with 'USERNAME_UNIQUE' index", result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
            Assert.IsInstanceOfType(result, typeof(ColumnIntegrityValidator));
        }

        [TestMethod]
        public void SpecificColumnOfTableShouldBePrimaryKey()
        {
            var tester = new IntegrityValidator();

            var connectionString = ConfigurationManager.ConnectionStrings["connectionString1"].ConnectionString;

            var result = tester.Database(connectionString).Table("SEC_USER").Column("ID").IsPrimaryKey();

            Assert.AreEqual("Ensure Table: 'SEC_USER' has primary key column: 'ID'", result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
            Assert.IsInstanceOfType(result, typeof(ColumnIntegrityValidator));
        }

        [TestMethod]
        public void SpecificColumnOfTableShouldBeForiegnKey()
        {
            var tester = new IntegrityValidator();

            var connectionString = ConfigurationManager.ConnectionStrings["connectionString1"].ConnectionString;

            var result = tester.Database(connectionString).Table("SEC_USERROLE").Column("ROLE_ID").IsForeignKey();

            Assert.AreEqual("Ensure Table: 'SEC_USERROLE' has foreign key column: 'ROLE_ID'", result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
            Assert.IsInstanceOfType(result, typeof(ColumnIntegrityValidator));
        }

        [TestMethod]
        public void SpecificColumnIsNullable()
        {
            var tester = new IntegrityValidator();

            var connectionString = ConfigurationManager.ConnectionStrings["connectionString1"].ConnectionString;

            var result = tester.Database(connectionString).Table("SEC_USER").Column("EMAIL").IsNullable(true);

            Assert.AreEqual("Ensure Table: 'SEC_USER' has nullable column: 'EMAIL'", result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
            Assert.IsInstanceOfType(result, typeof(ColumnIntegrityValidator));
        }

        [TestMethod]
        public void SpecificColumnIsNotNullable()
        {
            var tester = new IntegrityValidator();

            var connectionString = ConfigurationManager.ConnectionStrings["connectionString1"].ConnectionString;

            var result = tester.Database(connectionString).Table("SEC_USER").Column("ID").IsNullable(false);

            Assert.AreEqual("Ensure Table: 'SEC_USER' has nullable column: 'ID'", result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
            Assert.IsInstanceOfType(result, typeof(ColumnIntegrityValidator));
        }

        #endregion

        #region Stored Procedure

        [TestMethod]
        public void StoredProcedureValidatorMustReturnPassedResutInCaseOfExistingStoredProcedure()
        {
            var tester = new IntegrityValidator();
            var result = tester.Database(ConfigurationManager.ConnectionStrings["connectionString1"].ConnectionString).Procedure("TEST").Exists();

            Assert.AreEqual("Ensure the database has procedure: 'TEST'", result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result, typeof(ProcedureIntegrityValidator));
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
        }
        #endregion

        #region Sql

        [TestMethod]
        public void QueryMustHvesSpecificResult()
        {
            var tester = new IntegrityValidator();
            var database = tester.Database(ConfigurationManager.ConnectionStrings["connectionString1"].ConnectionString);
            var result = database.Sql("Select * From SEC_USER").Result;
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public void QueryMustReturnACollectionOfRows()
        {
            var tester = new IntegrityValidator();
            var database = tester.Database(ConfigurationManager.ConnectionStrings["connectionString1"].ConnectionString);
            var result = database.Sql("Select * From SEC_USER").Result;
            Assert.IsTrue(result.Any(q => q.USERNAME != string.Empty));
        }
        #endregion

        #region Sequence

        [TestMethod]
        public void SpecificTableOfDatabaseMustHaveSequence()
        {
            var tester = new IntegrityValidator();
            var database = tester.Database(ConfigurationManager.ConnectionStrings["connectionString1"].ConnectionString);
            var result = database.Sequence("NIMA").Exists();
            Assert.AreEqual("Ensure Sequence: 'NIMA' exists", result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
        }

        #endregion

        #region Index

        [TestMethod]
        public void DatabaseMustHaveIndex()
        {
            var tester = new IntegrityValidator();
            var database = tester.Database(ConfigurationManager.ConnectionStrings["connectionString1"].ConnectionString);
            var result = database.Index("USERNAME_UNIQUE").Exists();
            Assert.AreEqual("Ensure Index: 'USERNAME_UNIQUE' exists", result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
        }

        #endregion


    }
}

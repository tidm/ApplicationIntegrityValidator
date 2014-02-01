using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApplicationIntegrityValidator.Test
{
    [TestClass]
    public class AppSettingsIntegrityValidatorTests
    {
        [TestMethod]
        public void AppConfigExistsMustReturnPassedResultInCaseOfExistingAppSettings()
        {
            var tester = new IntegrityValidator();

            var appSettings = ConfigurationManager.AppSettings;
            if (appSettings.Count == 0)
                appSettings.Set("Key", "Value");
            var result = tester.AppConfig().AppSettings(appSettings.GetKey(0)).Exists();

            Assert.AreEqual("Ensure app setting with key: " + appSettings.GetKey(0) + " exists in web.config", result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
            Assert.IsInstanceOfType(result, typeof(AppSettingsIntegrityValidator));
        }

        [TestMethod]
        public void AppSettingsMustHaveValue()
        {
            var tester = new IntegrityValidator();

            var appSettings = ConfigurationManager.AppSettings;
            if (appSettings.Count == 0)
                appSettings.Set("Key", "Value");
            var result = tester.AppConfig().AppSettings(appSettings.GetKey(0)).ValueIs(appSettings.GetValues(0).First());

            Assert.AreEqual("Ensure app setting with key: " + appSettings.GetKey(0) + " has value: " + appSettings.GetValues(0).First() + " in web.config", result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
            Assert.IsInstanceOfType(result, typeof(AppSettingsIntegrityValidator));
        }

        [TestMethod]
        public void AppSettingsChainingTest()
        {
            var tester = new IntegrityValidator();
            var appSettings = ConfigurationManager.AppSettings;
            if (appSettings.Count == 0)
                appSettings.Set("Key", "Value");
            var result = tester.AppConfig().AppSettings(appSettings.GetKey(0)).Exists().ValueIs(appSettings.GetValues(0).First());

            Assert.AreEqual("Ensure app setting with key: " + appSettings.GetKey(0) + " exists in web.config", result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));

            Assert.AreEqual("Ensure app setting with key: " + appSettings.GetKey(0) + " has value: " + appSettings.GetValues(0).First() + " in web.config", result.Last().Description);
            Assert.IsTrue(result.Last().Succeed);
            Assert.IsNull(result.Last().Exception);
            Assert.IsInstanceOfType(result.Last(), typeof(IntegrityValidationResult));
            Assert.IsInstanceOfType(result, typeof(AppSettingsIntegrityValidator));

        }

        [TestMethod]
        public void AppConfigExistsMustReturnPassedResultInCaseOfExistingConnectionStrings()
        {
            var tester = new IntegrityValidator();

            var connectionSettings = ConfigurationManager.ConnectionStrings;
            if (connectionSettings.Count == 0)
                connectionSettings.Add(new ConnectionStringSettings("ConnectionString1", "Value"));
            var result = tester.AppConfig().ConnectionStrings(connectionSettings[0].Name).Exists();

            Assert.AreEqual("Ensure connection string with name: " + connectionSettings[0].Name + " exists in web.config", result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
            Assert.IsInstanceOfType(result, typeof(ConnectionStringsIntegrityValidator));
        }

        [TestMethod]
        public void ConnectionStringsMustHaveValue()
        {
            var tester = new IntegrityValidator();

            var connectionSettings = ConfigurationManager.ConnectionStrings;
            if (connectionSettings.Count == 0)
                connectionSettings.Add(new ConnectionStringSettings("ConnectionString1", "Value"));
            var result = tester.AppConfig().ConnectionStrings(connectionSettings[0].Name).ValueIs(connectionSettings[0].ConnectionString);

            Assert.AreEqual("Ensure connection string with name: " + connectionSettings[0].Name + " has connectionstring: " + connectionSettings[0].ConnectionString + " in web.config", result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
            Assert.IsInstanceOfType(result, typeof(ConnectionStringsIntegrityValidator));
        }

        [TestMethod]
        public void ConnectionStringsChainingTest()
        {
            var tester = new IntegrityValidator();
            var connectionStrings = ConfigurationManager.ConnectionStrings;
            if (connectionStrings.Count == 0)
                connectionStrings.Add(new ConnectionStringSettings("ConnectionString1", "Value"));
            var result = tester.AppConfig().ConnectionStrings(connectionStrings[0].Name).Exists().ValueIs(connectionStrings[0].ConnectionString);

            Assert.AreEqual("Ensure connection string with name: " + connectionStrings[0].Name + " exists in web.config", result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));

            Assert.AreEqual("Ensure connection string with name: " + connectionStrings[0].Name + " has connectionstring: " + connectionStrings[0].ConnectionString + " in web.config", result.Last().Description);
            Assert.IsTrue(result.Last().Succeed);
            Assert.IsNull(result.Last().Exception);
            Assert.IsInstanceOfType(result.Last(), typeof(IntegrityValidationResult));
            Assert.IsInstanceOfType(result, typeof(ConnectionStringsIntegrityValidator));

        }

    }
}

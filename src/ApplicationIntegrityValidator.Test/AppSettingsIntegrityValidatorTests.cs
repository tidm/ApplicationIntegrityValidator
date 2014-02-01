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
        public void AppSettingsExistsMustReturnPassedResultInCaseOfExistingAppSettings()
        {
            var tester = new IntegrityValidator();

            var appSettings = ConfigurationManager.AppSettings;
            if (appSettings.Count == 0)
                appSettings.Set("Key", "Value");
            var result = tester.AppSettings(appSettings.GetKey(0)).Exists();

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
            var result = tester.AppSettings(appSettings.GetKey(0)).ValueIs(appSettings.GetValues(0).First());

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
            var result = tester.AppSettings(appSettings.GetKey(0)).Exists().ValueIs(appSettings.GetValues(0).First());

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

    }
}

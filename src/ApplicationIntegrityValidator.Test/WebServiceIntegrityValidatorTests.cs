using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApplicationIntegrityValidator.Test
{
    [TestClass]
    public class WebServiceIntegrityValidatorTests
    {

        [TestMethod]
        public void EnsureAWebServiceIsStablished()
        {
            var tester = new IntegrityValidator();

            const string url = @"http://localhost:33657/api/UiTeam/GetStackHolders";
            var result = tester.WebService(url).Request("Get");

            Assert.AreEqual(string.Format("Ensure Web Service: '{0}' is stablished", url), result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
            Assert.IsInstanceOfType(result, typeof(WebServiceIntegrityValidator));
        }

        public void EnsureAWebServiceReturnSpecificStatucCode()
        {
            var tester = new IntegrityValidator();

            const string url = @"http://localhost:33657/api/UiTeam/Get";
            const HttpStatusCode statusCode = HttpStatusCode.OK;
            var result = tester.WebService(url).ReturnStatusCode(statusCode);

            Assert.AreEqual(string.Format("Ensure Web Service: '{0}' Returns StatusCode = '{1}'", url, statusCode), result.First().Description);
            Assert.IsTrue(result.First().Succeed);
            Assert.IsNull(result.First().Exception);
            Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
            Assert.IsInstanceOfType(result, typeof(WebServiceIntegrityValidator));
        }

        //[TestMethod]
        //public void EnsureAWebServiceReturnsSpecificValues()
        //{
        //    var tester = new IntegrityValidator();

        //    const string url = @"http://localhost:33657/api/UiTeam/Get/Zahedi";
        //    var result = tester.WebService(url).ReturnValueIs(new
        //                                                      {
        //                                                          id = "1",
        //                                                          name = "Nima",
        //                                                          family = "Zahedi"
        //                                                      });

        //    Assert.AreEqual(string.Format("Ensure Web Service: '{0}' is stablished", url), result.First().Description);
        //    Assert.IsTrue(result.First().Succeed);
        //    Assert.IsNull(result.First().Exception);
        //    Assert.IsInstanceOfType(result.First(), typeof(IntegrityValidationResult));
        //    Assert.IsInstanceOfType(result, typeof(WebServiceIntegrityValidator));
        //}
    }
}

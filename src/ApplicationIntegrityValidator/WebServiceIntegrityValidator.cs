using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Script.Serialization;

namespace ApplicationIntegrityValidator
{
    public class WebServiceIntegrityValidator : IEnumerable<IntegrityValidationResult>
    {

        private readonly List<IntegrityValidationResult> _results;
        private readonly string _uri;

        public WebServiceIntegrityValidator(string uri)
        {
            _results = new List<IntegrityValidationResult>();
            _uri = uri;
        }

        public WebServiceIntegrityValidator Request(string verb, object parameters = null)
        {
            var httpClient = new HttpClient();
            var result = httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, _uri), HttpCompletionOption.ResponseContentRead).Result;
            //_results.Add(result);
            //var statusCode = result.StatusCode;
            //var reasonPhrase = result.ReasonPhrase;
            _results.Add(new IntegrityValidationResult()
                                     {
                                         Exception = null,
                                         Description = string.Format("Ensure Web Service: '{0}' is stablished", _uri),
                                         Succeed = !(result.StatusCode >= HttpStatusCode.BadRequest && result.StatusCode <= HttpStatusCode.HttpVersionNotSupported)
                                     });
            return this;
        }

        public WebServiceIntegrityValidator ReturnStatusCode(HttpStatusCode statusCode)
        {
            var httpClient = new HttpClient();
            var result = httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, _uri), HttpCompletionOption.ResponseContentRead).Result;
            _results.Add(new IntegrityValidationResult()
            {
                Exception = null,
                Description = string.Format("Ensure Web Service: '{0}' is stablished", _uri),
                Succeed = result.StatusCode == statusCode
            });
            return this;
        }

        //public WebServiceIntegrityValidator ReturnValueIs(dynamic value)
        //{
        //    var httpClient = new HttpClient();
        //    var result = httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, _uri), HttpCompletionOption.ResponseContentRead).Result;

        //    var content = result.Content.ReadAsStringAsync();
        //    var json = new JavaScriptSerializer();
        //    var lsContent = json.Deserialize<dynamic>(content.Result);
        //    var resultWS = lsContent as DynamicObject;
        //    var resultWSCount = resultWS.GetDynamicMemberNames().Count();

        //    foreach (var prop in resultWS.GetDynamicMemberNames().equ)
        //    {
                
        //    }

        //    _results.Add(new IntegrityValidationResult()
        //    {
        //        Exception = null,
        //        Description = string.Format("Ensure Web Service: '{0}' returns specific value", _uri),
        //        Succeed = lsContent == value
        //    });
        //    return this;
        //}

        //public WebServiceIntegrityValidator ReturnValueIs(List<string> values)
        //{
        //    var httpClient = new HttpClient();
        //    var result = httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, _uri), HttpCompletionOption.ResponseContentRead).Result;

        //    var content = result.Content.ReadAsStringAsync();
        //    var json = new JavaScriptSerializer();
        //    var lsContent = json.Deserialize<List<string>>(content.Result);

        //    _results.Add(new IntegrityValidationResult()
        //    {
        //        Exception = null,
        //        Description = string.Format("Ensure Web Service: '{0}' returns specific values", _uri),
        //        Succeed = lsContent == values
        //    });
        //    return this;
        //}

        public IEnumerator<IntegrityValidationResult> GetEnumerator()
        {
            return _results.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _results.GetEnumerator();
        }
    }
}

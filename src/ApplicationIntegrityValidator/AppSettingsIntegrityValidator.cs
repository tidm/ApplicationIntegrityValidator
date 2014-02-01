using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace ApplicationIntegrityValidator
{
    public class AppSettingsIntegrityValidator : IEnumerable<IntegrityValidationResult>
    {
        private readonly List<IntegrityValidationResult> _results;
        private readonly string _key;

        public AppSettingsIntegrityValidator(string key)
        {
            _key = key;
            _results = new List<IntegrityValidationResult>();


        }

        public AppSettingsIntegrityValidator Exists()
        {
            var result = new IntegrityValidationResult()
            {
                Description = string.Format("Ensure app setting with key: {0} exists in web.config", _key),
                Succeed = ConfigurationManager.AppSettings[_key] != null,
                Exception = null
            };
            _results.Add(result);
            return this;
        }

        public AppSettingsIntegrityValidator ValueIs(string value)
        {
            var result = new IntegrityValidationResult()
            {
                Description = string.Format("Ensure app setting with key: {0} has value: {1} in web.config", _key, value),
                Succeed = ConfigurationManager.AppSettings[_key] == value,
                Exception = null
            };
            _results.Add(result);
            return this;
        }

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

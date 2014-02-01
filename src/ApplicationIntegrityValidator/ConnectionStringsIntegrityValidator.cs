using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace ApplicationIntegrityValidator
{
    public class ConnectionStringsIntegrityValidator : IEnumerable<IntegrityValidationResult>
    {

        private readonly List<IntegrityValidationResult> _results;
        private readonly string _name;

        public ConnectionStringsIntegrityValidator(string name)
        {
            _name = name;
            _results = new List<IntegrityValidationResult>();
        }

        public ConnectionStringsIntegrityValidator Exists()
        {
            var result = new IntegrityValidationResult()
            {
                Description = string.Format("Ensure connection string with name: {0} exists in web.config", _name),
                Succeed = ConfigurationManager.ConnectionStrings[_name] != null,
                Exception = null
            };
            _results.Add(result);
            return this;
        }

        public ConnectionStringsIntegrityValidator ValueIs(string connectionString)
        {
            var result = new IntegrityValidationResult()
            {
                Description = string.Format("Ensure connection string with name: {0} has connectionstring: {1} in web.config", _name, connectionString),
                Succeed = ConfigurationManager.ConnectionStrings[_name].ConnectionString == connectionString,
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

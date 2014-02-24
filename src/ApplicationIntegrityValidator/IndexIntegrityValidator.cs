using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Codeplex.Data;

namespace ApplicationIntegrityValidator
{
    public class IndexIntegrityValidator: IEnumerable<IntegrityValidationResult>
    {
        private readonly string _indexName;
        private readonly string _connectionString;
        private readonly List<IntegrityValidationResult> _results;

        public IndexIntegrityValidator(string connectionString, string indexName)
        {
            _indexName = indexName;
            _connectionString = connectionString;
            _results = new List<IntegrityValidationResult>();
        }

        public IndexIntegrityValidator Exists()
        {
            var index = DbExecutor.ExecuteReader(
                new OleDbConnection(_connectionString), string.Format("select * from user_indexes where INDEX_NAME = '{0}'", _indexName)).Count();
            var result = new IntegrityValidationResult()
            {
                Description = string.Format("Ensure Index: '{0}' exists", _indexName),
                Succeed = index == 1,
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

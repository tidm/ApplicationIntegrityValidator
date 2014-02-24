using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using Codeplex.Data;

namespace ApplicationIntegrityValidator
{
    public class SequenceIntegrityValidator : IEnumerable<IntegrityValidationResult>
    {

        private readonly string _sequenceName;
        private readonly string _connectionString;
        private readonly List<IntegrityValidationResult> _results;

        public SequenceIntegrityValidator(string connectionString, string sequenceName)
        {
            _sequenceName = sequenceName;
            _connectionString = connectionString;
            _results = new List<IntegrityValidationResult>();
        }

        public SequenceIntegrityValidator Exists()
        {
            var sequence = DbExecutor.ExecuteReader(
                new OleDbConnection(_connectionString), string.Format("select * from USER_SEQUENCES where Sequence_Name = '{0}'", _sequenceName)).Count();
            var result = new IntegrityValidationResult()
            {
                Description = string.Format("Ensure Sequence: '{0}' exists", _sequenceName),
                Succeed = sequence == 1,
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

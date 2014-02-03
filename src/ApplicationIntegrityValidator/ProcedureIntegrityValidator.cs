using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Codeplex.Data;

namespace ApplicationIntegrityValidator
{
    public class ProcedureIntegrityValidator : IEnumerable<IntegrityValidationResult>
    {

        private readonly string _procedureName;
        private readonly string _connectionString;
        private readonly List<IntegrityValidationResult> _results;

        public ProcedureIntegrityValidator(string connectionString, string procedureName)
        {
            _connectionString = connectionString;
            _procedureName = procedureName;
            _results = new List<IntegrityValidationResult>();
        }

        public ProcedureIntegrityValidator Exists()
        {
            var procedure = DbExecutor.ExecuteReader(
                new OleDbConnection(_connectionString),
                string.Format("select * from user_source where line = 1 and type = 'PROCEDURE' and name = '{0}'", _procedureName)).Count();

            var result = new IntegrityValidationResult()
            {
                Description = string.Format("Ensure the database has procedure: '{0}'", _procedureName),
                Succeed = procedure == 1,
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

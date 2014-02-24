using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace ApplicationIntegrityValidator
{
    public class DatabaseIntegrityValidator
    {
        private readonly string _connectionString;
        private readonly List<IntegrityValidationResult> _results;

        public DatabaseIntegrityValidator(string connectionString)
        {
            _connectionString = connectionString;
            _results = new List<IntegrityValidationResult>();
        }

        public TableIntegrityValidator Table(string tableName)
        {
            return new TableIntegrityValidator(tableName, _connectionString);
        }

        public ProcedureIntegrityValidator Procedure(string procedureName)
        {
            return new ProcedureIntegrityValidator(_connectionString, procedureName);
        }

        public SqlIntegrityValidator Sql(string query)
        {
            return new SqlIntegrityValidator(_connectionString, query);
        }

        public SequenceIntegrityValidator Sequence(string sequenceName)
        {
            return new SequenceIntegrityValidator(_connectionString, sequenceName);
        }

        public IndexIntegrityValidator Index(string indexName)
        {
            return new IndexIntegrityValidator(_connectionString, indexName);
        }

    }
}

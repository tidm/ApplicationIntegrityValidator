using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Codeplex.Data;

namespace ApplicationIntegrityValidator
{
    public class TableIntegrityValidator : IEnumerable<IntegrityValidationResult>
    {

        private readonly string _tableName;
        private readonly string _connectionString;
        private readonly List<IntegrityValidationResult> _results;

        public TableIntegrityValidator(string tableName, string connectionString)
        {
            _connectionString = connectionString;
            _tableName = tableName;
            _results = new List<IntegrityValidationResult>();
        }

        public TableIntegrityValidator Exists()
        {
            var table = DbExecutor.ExecuteReader(
                new OleDbConnection(_connectionString),
                string.Format("SELECT table_name FROM user_tables where table_name = '{0}'", _tableName)).Count();
            var result = new IntegrityValidationResult()
                         {
                             Description = string.Format("Ensure Table: '{0}' exists in database", _tableName),
                             Succeed = table == 1,
                             Exception = null
                         };
            _results.Add(result);
            return this;
        }

        public TableIntegrityValidator HasColumns(List<string> columns)
        {
            var cols = DbExecutor.ExecuteReader(
                new OleDbConnection(_connectionString), string.Format("SELECT COLUMN_NAME FROM USER_TAB_COLUMNS where table_name = '{0}'", "SEC_USER")).Select(t => new
                {
                    name = (string)t["COLUMN_NAME"]
                }).ToList();

            var names = columns[0];
            for (var i = 1; i < columns.Count; i++)
            {
                names += ", " + columns[i];
            }

            var res = true;
            foreach (var col in columns)
            {
                if (cols.All(c => c.name != col))
                    res = false;
            }

            var result = new IntegrityValidationResult()
            {
                Description = string.Format("Ensure Table: 'SEC_USER' has these columns: {0}", names),
                Succeed = res,
                Exception = null
            };
            _results.Add(result);
            return this;
        }

        public TableIntegrityValidator RowCount(Func<int, bool> func)
        {
            var rowCount = DbExecutor.ExecuteScalar<decimal>(
                new OleDbConnection(_connectionString),
                string.Format("SELECT count(*) FROM {0}", _tableName));
            var result = new IntegrityValidationResult()
            {
                Description = string.Format("Ensure Table: '{0}' has rows", _tableName),
                Succeed = func((int)rowCount),
                Exception = null
            };
            _results.Add(result);
            return this;
        }

        public ColumnIntegrityValidator Column(string columnName)
        {
            return new ColumnIntegrityValidator(_connectionString, _tableName, columnName);
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

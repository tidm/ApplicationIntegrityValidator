using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Codeplex.Data;

namespace ApplicationIntegrityValidator
{
    public class ColumnIntegrityValidator : IEnumerable<IntegrityValidationResult>
    {

        private readonly string _columnName;
        private readonly string _tableName;
        private readonly string _connectionString;
        private readonly List<IntegrityValidationResult> _results;

        public ColumnIntegrityValidator(string connectionString, string tableName, string columnName)
        {
            _connectionString = connectionString;
            _tableName = tableName;
            _columnName = columnName;
            _results = new List<IntegrityValidationResult>();
        }

        public ColumnIntegrityValidator Exists()
        {
            var column = DbExecutor.ExecuteReader(
                new OleDbConnection(_connectionString),
                string.Format("SELECT column_name FROM user_tab_columns where table_name = '{0}' and column_name = '{1}'", _tableName, _columnName)).Count();

            var result = new IntegrityValidationResult()
            {
                Description = string.Format("Ensure Table: '{0}' has '{1}' column", _tableName, _columnName),
                Succeed = column == 1,
                Exception = null
            };
            _results.Add(result);
            return this;
        }

        public ColumnIntegrityValidator TypeIs(OracleDataType type, int size)
        {
            var column = DbExecutor.ExecuteReader(

                new OleDbConnection(_connectionString),
                string.Format("SELECT data_type, data_length FROM user_tab_columns where table_name = '{0}' and column_name = '{1}' and data_type = '{2}' and data_length = {3}", _tableName, _columnName, type, size)).Count();
            var result = new IntegrityValidationResult()
            {
                Description = string.Format("Ensure Table: '{0}' has '{1}' column with '{2}' data type and size '{3}'", _tableName, _columnName, type, size),
                Succeed = column == 1,
                Exception = null
            };
            _results.Add(result);
            return this;
        }

        public ColumnIntegrityValidator HasIndex(string indexName)
        {
            var index = DbExecutor.ExecuteReader(
                new OleDbConnection(_connectionString),
                string.Format("select INDEX_NAME from USER_INDEXES where table_name = '{0}' and INDEX_NAME = '{1}'", _tableName, indexName)).Count();

            var result = new IntegrityValidationResult()
            {
                Description = string.Format("Ensure Table: '{0}' has '{1}' column with '{2}' index", _tableName, _columnName, indexName),
                Succeed = index == 1,
                Exception = null
            };
            _results.Add(result);
            return this;
        }

        public ColumnIntegrityValidator IsPrimaryKey()
        {
            var primaryKey = DbExecutor.ExecuteReader(
                new OleDbConnection(_connectionString),
                string.Format("SELECT cols.column_name FROM user_constraints cons, all_cons_columns cols WHERE cols.table_name = '{0}' AND cons.constraint_type = 'P' AND cons.constraint_name = cols.constraint_name AND cons.owner = cols.owner ORDER BY cols.table_name, cols.position", _tableName)).Count();

            var result = new IntegrityValidationResult()
            {
                Description = string.Format("Ensure Table: '{0}' has primary key column: '{1}'", _tableName, _columnName),
                Succeed = primaryKey == 1,
                Exception = null
            };
            _results.Add(result);
            return this;
        }

        public ColumnIntegrityValidator IsForeignKey()
        {
            var foreign = DbExecutor.ExecuteReader(
                new OleDbConnection(_connectionString),
                string.Format("SELECT uc.column_name FROM all_cons_columns a JOIN all_constraints c ON a.owner = c.owner AND a.constraint_name = c.constraint_name JOIN all_constraints c_pk ON c.r_owner = c_pk.owner AND c.r_constraint_name = c_pk.constraint_name join USER_CONS_COLUMNS uc on uc.constraint_name = c.r_constraint_name where a.table_name = '{0}' and a.column_name = '{1}'", _tableName, _columnName)).Count();

            var result = new IntegrityValidationResult()
            {
                Description = string.Format("Ensure Table: '{0}' has foreign key column: '{1}'", _tableName, _columnName),
                Succeed = foreign != 0,
                Exception = null
            };
            _results.Add(result);
            return this;
        }

        public ColumnIntegrityValidator IsNullable(bool isNullable)
        {
            var res = DbExecutor.ExecuteReader(
                new OleDbConnection(_connectionString),
                string.Format("SELECT Nullable FROM user_tab_columns where table_name = '{0}' and column_name = '{1}'", _tableName, _columnName)).Select(t => new
                {
                    nullable = (string)t["NULLABLE"],
                }).SingleOrDefault();

            var result = new IntegrityValidationResult()
            {
                Description = string.Format("Ensure Table: '{0}' has nullable column: '{1}'", _tableName, _columnName),
                Succeed = res != null && res.nullable == (isNullable ? "Y" : "N"),
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Dynamic;
using System.Linq;
using System.Text;
using Codeplex.Data;

namespace ApplicationIntegrityValidator
{
    public class SqlIntegrityValidator
    {

        private readonly string _query;
        private readonly string _connectionString;

        public SqlIntegrityValidator(string connectionString, string query)
        {
            _connectionString = connectionString;
            _query = query;
        }

        public IEnumerable<dynamic> Result
        {
            get
            {
                using (var con = new OleDbConnection(_connectionString))
                {
                    var result = DbExecutor.ExecuteReaderDynamic(con, _query).Select(q =>
                                                                                     {
                                                                                         var columns = q.GetDynamicMemberNames();
                                                                                         var rows = new ExpandoObject() as IDictionary<string, object>;
                                                                                         foreach (var column in columns)
                                                                                         {
                                                                                             rows.Add(column, q[column]);
                                                                                         }
                                                                                         return rows;
                                                                                     }).ToList();
                    return result;
                }
            }
        }

    }
}

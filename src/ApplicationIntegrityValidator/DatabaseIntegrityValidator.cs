﻿using System;
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

    }
}

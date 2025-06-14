﻿

using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace MTKDotNetCore.Shared
{
    public class DapperService
    {
        protected readonly string _connectionString;

        public DapperService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> Query<T>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            return db.Query<T>(query, param).ToList();
        }

        public int Execute(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            return db.Execute(query, param);
        }
    }
}

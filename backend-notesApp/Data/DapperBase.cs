using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using backend_notesApp.Models;

namespace backend_notesApp.Data
{
    public class DapperBase
    {
        private readonly string _connectionString;

        // Constructor to accept connection string
        public DapperBase(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Execute queries like INSERT, UPDATE, DELETE
        public void Execute(string sql, DynamicParameters param = null)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                db.Execute(sql, param);
            }
        }

        // Query single record (select one)
        public T QuerySingle<T>(string sql, DynamicParameters param = null)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                return db.QuerySingleOrDefault<T>(sql, param);
            }
        }

        // Query multiple records (select many)
        public IEnumerable<T> Query<T>(string sql, DynamicParameters param = null)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                return db.Query<T>(sql, param);
            }
        }
    }
}

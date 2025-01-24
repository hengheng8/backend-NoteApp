using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using backend_notesApp.Models;

namespace backend_notesApp.Controllers
{
    public class DapperORM
    {
        private readonly string _connectionString;

        // Constructor to accept connection string
        public DapperORM(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Execute queries like INSERT, UPDATE, DELETE
        public void Execute(string sql, object param = null)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                db.Execute(sql, param);
            }
        }

        // Query single record (select one)
        public T QuerySingle<T>(string sql, object param = null)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                return db.QuerySingleOrDefault<T>(sql, param);
            }
        }

        // Query multiple records (select many)
        public IEnumerable<T> Query<T>(string sql, object param = null)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                return db.Query<T>(sql, param);
            }
        }

        // Create a note
        public void CreateNote(Note note)
        {
            // You can pass an anonymous object instead of DynamicParameters
            var parameters = new { Title = note.Title, Content = note.Content };

            // Insert SQL query
            string sql = "INSERT INTO Notes (Title, Content) VALUES (@Title, @Content)";
            Execute(sql, parameters);
        }

        // Get all notes
        public IEnumerable<Note> GetNotes()
        {
            string sql = "SELECT * FROM Notes";
            return Query<Note>(sql);
        }

        // Get a specific note by ID
        public Note GetNoteById(int id)
        {
            var parameters = new { Id = id };

            string sql = "SELECT * FROM Notes WHERE Id = @Id";
            return QuerySingle<Note>(sql, parameters);
        }

        // Update a note
        public void UpdateNote(Note note)
        {
            var parameters = new { Id = note.Id, Title = note.Title, Content = note.Content };

            string sql = "UPDATE Notes SET Title = @Title, Content = @Content WHERE Id = @Id";
            Execute(sql, parameters);
        }

        // Delete a note
        public void DeleteNote(int id)
        {
            var parameters = new { Id = id };

            string sql = "DELETE FROM Notes WHERE Id = @Id";
            Execute(sql, parameters);
        }
    }
}

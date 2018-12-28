using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using SQLite;
using UseCases;

namespace Infrastructure {
    public class SqlLiteLogger : ILogger {
        private const string DefaultDatabaseName = "sqliteprod";
        private readonly SQLiteConnection _connection;

        public SqlLiteLogger(string databaseName = null) {
            _connection = new SQLiteConnection(databaseName ?? DefaultDatabaseName);
            _connection.CreateTable<SqliteLog>();
        }

        public void Log(string message) {
            var log = new SqliteLog {
                Message = message
            };
            var foo = _connection.Insert(log);
            if (foo != 1) throw new Exception("Insert failed!");
        }

        public string GetLoggedGuesses() {
            const string getAllMessagesQuery = "select * from SqliteLog";
            List<SqliteLog> logs = _connection.Query<SqliteLog>(getAllMessagesQuery);
            IEnumerable<string> messages = logs.Select(x => x.Message);
            return string.Join("\n", messages);
        }

        public void ClearLog() {
            _connection.DropTable<SqliteLog>();
        }
    }
}
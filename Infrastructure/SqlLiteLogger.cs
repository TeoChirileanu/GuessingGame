using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using SQLite;
using UseCases;

namespace Infrastructure {
    public class SqlLiteLogger : ILogger {
        private const string DefaultDatabaseName = "sqliteprod";
        private readonly SQLiteAsyncConnection _connection;

        public SqlLiteLogger(string databaseName = null) {
            _connection = new SQLiteAsyncConnection(databaseName ?? DefaultDatabaseName);
            _connection.CreateTableAsync<SqliteLog>();
        }

        public async Task Log(string message) {
            var log = new SqliteLog {
                Message = message
            };
            await _connection.InsertAsync(log);
        }

        public async Task<string> GetLoggedGuesses() {
            const string getAllMessagesQuery = "select * from SqliteLog";
            List<SqliteLog> logs = await _connection.QueryAsync<SqliteLog>(getAllMessagesQuery);
            IEnumerable<string> messages = logs.Select(x => x.Message);
            return string.Join("\n", messages);
        }

        public async Task ClearLog() {
            await _connection.DropTableAsync<SqliteLog>();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Common;
using LiteDB;
using UseCases;

namespace Infrastructure {
    public class LiteDbLogger : ILogger {
        public string CollectionName { private get; set; } = "LogCollection";
        public string DatabaseName { private get; set; } = "log.litedb";

        public void Log(string message) {
            using (var liteDatabase = new LiteDatabase(DatabaseName)) {
                LiteCollection<LiteDbLog> logCollection =
                    liteDatabase.GetCollection<LiteDbLog>(CollectionName);
                var messageDto = new LiteDbLog {
                    Message = message
                };
                logCollection.Upsert(messageDto);
            }
        }

        public string GetLoggedGuesses() {
            using (var liteDatabase = new LiteDatabase(DatabaseName)) {
                LiteCollection<LiteDbLog> logCollection =
                    liteDatabase.GetCollection<LiteDbLog>(CollectionName);
                IEnumerable<LiteDbLog> logs = logCollection.FindAll();
                IEnumerable<string> messages = logs.Select(x => x.Message);
                return string.Join("\n", messages);
            }
        }

        public void ClearLog() {
            using (var liteDatabase = new LiteDatabase(DatabaseName)) {
                LiteCollection<LiteDbLog> logCollection =
                    liteDatabase.GetCollection<LiteDbLog>(CollectionName);
                logCollection.Delete(Query.All());
            }
        }
    }
}
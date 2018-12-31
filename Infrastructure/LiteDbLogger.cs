using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using LiteDB;
using UseCases;

namespace Infrastructure {
    public class LiteDbLogger : ILogger {
        public string CollectionName { private get; set; } = "LogCollection";
        public string DatabaseName { private get; set; } = "log.litedb";

        public async Task Log(string message) {
            using (var liteDatabase = new LiteDatabase(DatabaseName)) {
                LiteCollection<LiteDbLog> logCollection =
                    liteDatabase.GetCollection<LiteDbLog>(CollectionName);
                var messageDto = new LiteDbLog {
                    Message = message
                };
                logCollection.Upsert(messageDto);
            }

            await Task.CompletedTask;
        }

        public async Task<string> GetLoggedGuesses() {
            using (var liteDatabase = new LiteDatabase(DatabaseName)) {
                LiteCollection<LiteDbLog> logCollection =
                    liteDatabase.GetCollection<LiteDbLog>(CollectionName);
                IEnumerable<LiteDbLog> logs = logCollection.FindAll();
                IEnumerable<string> messages = logs.Select(x => x.Message);
                var result = string.Join("\n", messages);
                return await Task.FromResult(result);
            }
        }

        public async Task ClearLog() {
            using (var liteDatabase = new LiteDatabase(DatabaseName)) {
                LiteCollection<LiteDbLog> logCollection =
                    liteDatabase.GetCollection<LiteDbLog>(CollectionName);
                logCollection.Delete(Query.All());
            }

            await Task.CompletedTask;
        }
    }
}
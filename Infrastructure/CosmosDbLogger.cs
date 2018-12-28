using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Common;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using UseCases;

namespace Infrastructure {
    public class CosmosDbLogger : ILogger {
        private const string PrimaryKey =
            "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

        private const string DatabaseId = "LogDatabase";
        private const string CollectionId = "LogCollection";

        private static readonly Uri EndPoint = new Uri("https://localhost:8081");
        private static readonly DocumentClient Client = new DocumentClient(EndPoint, PrimaryKey);

        public CosmosDbLogger() {
            var database = new Database {
                Id = DatabaseId
            };
            Client.CreateDatabaseIfNotExistsAsync(database);

            var databaseUri = UriFactory.CreateDatabaseUri(database.Id);
            var collection = new DocumentCollection {
                Id = CollectionId
            };
            Client.CreateDocumentCollectionIfNotExistsAsync(databaseUri, collection);
        }

        public void Log(string message) {
            var log = new CosmosDbLog {
                Id = Guid.NewGuid().ToString(),
                Message = message
            };
            var uri = GetDocumentCollectionUri();
            Client.UpsertDocumentAsync(uri, log);
        }

        public string GetLoggedGuesses() {
            IEnumerable<CosmosDbLog> logs = GetLogs();
            IEnumerable<string> messages = logs.Select(x => x.Message);
            return string.Join("\n", messages);
        }

        public async void ClearLog() {
            var databaseUri = UriFactory.CreateDatabaseUri(DatabaseId);
            await Client.DeleteDatabaseAsync(databaseUri);
        }

        private IEnumerable<CosmosDbLog> GetLogs() {
            var uri = GetDocumentCollectionUri();
            IDocumentQuery<CosmosDbLog> query = Client
                .CreateDocumentQuery<CosmosDbLog>(uri).AsDocumentQuery();
            ICollection<CosmosDbLog> logs = new Collection<CosmosDbLog>();
            while (query.HasMoreResults) {
                FeedResponse<CosmosDbLog> results = query.ExecuteNextAsync<CosmosDbLog>().Result;
                foreach (var result in results) logs.Add(result);
            }

            return logs;
        }

        private static Uri GetDocumentCollectionUri() =>
            UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId);
    }
}
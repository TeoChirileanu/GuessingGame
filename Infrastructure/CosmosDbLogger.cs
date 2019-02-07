using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using UseCases;

namespace Infrastructure {
    public class CosmosDbLogger : ILogger, IAsyncInitialization {
        private const string PrimaryKey =
            "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

        private const string DatabaseId = "LogDatabase";
        private const string CollectionId = "LogCollection";

        private static readonly Uri EndPoint = new Uri(Resources.LocalHostUri);
        private static readonly DocumentClient Client = new DocumentClient(EndPoint, PrimaryKey);
        public CosmosDbLogger() => Initialization = InitializeAsync();

        public Task Initialization { get; }

        public async Task Log(string message) {
            var log = new CosmosDbLog {
                Id = Guid.NewGuid().ToString(),
                Message = message
            };
            var uri = GetDocumentCollectionUri();
            await Client.UpsertDocumentAsync(uri, log);
        }

        public async Task<string> GetLoggedGuesses() {
            IEnumerable<CosmosDbLog> logs = GetLogs();
            IEnumerable<string> messages = logs.Select(x => x.Message);
            var result = string.Join("\n", messages);
            return await Task.FromResult(result);
        }

        public async Task ClearLog() {
            var databaseUri = UriFactory.CreateDatabaseUri(DatabaseId);
            await Client.DeleteDatabaseAsync(databaseUri);
        }

        private static IEnumerable<CosmosDbLog> GetLogs() {
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

        private static async Task InitializeAsync() {
            var database = new Database {
                Id = DatabaseId
            };
            await Client.CreateDatabaseIfNotExistsAsync(database);

            var databaseUri = UriFactory.CreateDatabaseUri(database.Id);
            var collection = new DocumentCollection {
                Id = CollectionId
            };
            await Client.CreateDocumentCollectionIfNotExistsAsync(databaseUri, collection);
        }
    }
}
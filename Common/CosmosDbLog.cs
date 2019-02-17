namespace GuessingGame.Common {
    public class CosmosDbLog : ILog<string> {
        public string Id { get; set; }
        public string Message { get; set; }
    }
}
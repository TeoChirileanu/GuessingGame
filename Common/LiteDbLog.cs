using LiteDB;

namespace GuessingGame.Common {
    public class LiteDbLog : ILog<ObjectId> {
        public ObjectId Id { get; set; }
        public string Message { get; set; }
    }
}
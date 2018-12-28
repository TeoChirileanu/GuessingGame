using SQLite;

namespace Common {
    public class SqliteLog : ILog<int> {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public string Message { get; set; }
    }
}
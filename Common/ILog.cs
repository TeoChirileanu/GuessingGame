namespace Common {
    public interface ILog<T> {
        T Id { get; set; }
        string Message { get; set; }
    }
}
namespace UseCases {
    public interface ILogger {
        void Log(string message);
        string GetLoggedGuesses();
        void ClearLog();
    }
}
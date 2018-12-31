using System.Threading.Tasks;

namespace UseCases {
    public interface ILogger {
        Task Log(string message);
        Task<string> GetLoggedGuesses();
        Task ClearLog();
    }
}
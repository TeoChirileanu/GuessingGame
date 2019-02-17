using System.Threading.Tasks;

namespace GuessingGame.UseCases {
    public interface ILogger {
        Task Log(string message);
        Task<string> GetLoggedGuesses();
        Task ClearLog();
    }
}
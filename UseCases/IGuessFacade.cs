using System.Threading.Tasks;

namespace GuessingGame.UseCases {
    public interface IGuessFacade {
        Task<int> GetGuessedNumber();
        Task<string> CheckGuessedNumber(int guessedNumber);
        Task DeliverGuessResult(string guessResult);
        Task DeliverLoggedGuesses();
    }
}
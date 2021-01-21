using System.Threading.Tasks;

namespace GuessingGame.UseCases {
    public interface INumberGetter {
        Task<int?> GetGuessedNumber();
    }
}
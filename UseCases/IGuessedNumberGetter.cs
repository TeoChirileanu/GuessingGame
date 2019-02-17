using System.Threading.Tasks;

namespace GuessingGame.UseCases {
    public interface IGuessedNumberGetter {
        Task<int?> GetGuessedNumber();
    }
}
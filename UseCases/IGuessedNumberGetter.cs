using System.Threading.Tasks;

namespace UseCases {
    public interface IGuessedNumberGetter {
        Task<int?> GetGuessedNumber();
    }
}
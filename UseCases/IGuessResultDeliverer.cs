using System.Threading.Tasks;

namespace GuessingGame.UseCases {
    public interface IDeliverer {
        Task Deliver(string guessResult);
    }
}
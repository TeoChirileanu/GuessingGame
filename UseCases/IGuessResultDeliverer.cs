using System.Threading.Tasks;

namespace GuessingGame.UseCases {
    public interface INumberDeliverer {
        Task Deliver(string guessResult);
    }
}
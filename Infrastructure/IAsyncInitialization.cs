using System.Threading.Tasks;

namespace GuessingGame.Infrastructure {
    public interface IAsyncInitialization {
        Task Initialization { get; }
    }
}
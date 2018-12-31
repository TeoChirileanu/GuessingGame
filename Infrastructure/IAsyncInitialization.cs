using System.Threading.Tasks;

namespace Infrastructure {
    public interface IAsyncInitialization {
        Task Initialization { get; }
    }
}
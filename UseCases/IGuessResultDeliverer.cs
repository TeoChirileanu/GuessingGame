using System.Threading.Tasks;

namespace UseCases {
    public interface IDeliverer {
        Task Deliver(string message);
    }
}
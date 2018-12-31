using System.Threading.Tasks;

namespace BusinessRules {
    public interface INumberChecker {
        Task<string> CheckNumber(int number);
    }
}
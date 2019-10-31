using System.Threading.Tasks;

namespace GuessingGame.BusinessRules {
    public interface INumberChecker {
        Task<string> CheckNumber(int number);
        
    }
}
using System;
using System.Threading.Tasks;
using GuessingGame.Common;

namespace GuessingGame.BusinessRules {
    public class NumberChecker : INumberChecker {
        private readonly int _correctNumber;

        public NumberChecker(int correctNumber) => _correctNumber = correctNumber;

        public NumberChecker() => _correctNumber = Resources.GetRandomNumber();

        public async Task<string> CheckNumber(int number) {
            NumberValidator.ValidateNumber(number);
            var comparisonResult = number.CompareTo(_correctNumber);
            string result;
            switch (comparisonResult) {
                case -1:
                    result = Resources.TooLowMessage;
                    break;
                case 1:
                    result = Resources.TooHighMessage;
                    break;
                case 0:
                    result = Resources.CorrectMessage;
                    break;
                default:
                    return null;
            }

            return await Task.FromResult(result);
        }
    }
}
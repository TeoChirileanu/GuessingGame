using System;
using Common;

namespace BusinessRules {
    public class NumberChecker : INumberChecker {
        private readonly int _correctNumber;

        public NumberChecker(int correctNumber) => _correctNumber = correctNumber;

        public NumberChecker() => _correctNumber = Resources.GetRandomNumber();

        public string CheckNumber(int number) {
            NumberValidator.ValidateNumber(number);
            var comparisonResult = number.CompareTo(_correctNumber);
            switch (comparisonResult) {
                case -1:
                    return Resources.TooLowMessage;
                case 1:
                    return Resources.TooHighMessage;
                case 0:
                    return Resources.CorrectMessage;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
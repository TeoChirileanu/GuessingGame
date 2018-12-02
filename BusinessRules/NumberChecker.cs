using System;
using Common;

namespace BusinessRules {
    public class NumberChecker : INumberChecker {
        private readonly int CorrectNumber;

        public NumberChecker(int correctNumber) => CorrectNumber = correctNumber;

        public NumberChecker() => CorrectNumber = Resources.CorrectNumber;

        public string CheckNumber(int number) {
            NumberValidator.ValidateNumber(number);
            var comparisonResult = number.CompareTo(CorrectNumber);
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
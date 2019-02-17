using System;

namespace GuessingGame.Common {
    public static class NumberValidator {
        public static void ValidateNumber(int number) {
            if (number < Resources.LowerBound || number > Resources.UpperBound)
                throw new ArgumentOutOfRangeException(Resources.NumberOutOfBoundsMessage);
        }

        public static int? GetIntValue(this string numberAsString) {
            var parsedSuccessfully = int.TryParse(numberAsString, out var parsedNumber);
            if (!parsedSuccessfully) return null;

            return parsedNumber;
        }
    }
}
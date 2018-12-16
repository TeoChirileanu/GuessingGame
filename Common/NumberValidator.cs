using System;

namespace Common {
    public static class NumberValidator {
        public static void ValidateNumber(int number) {
            if (number < Resources.LowerBound || number > Resources.UpperBound)
                throw new ArgumentOutOfRangeException(Resources.NumberOutOfBoundsMessage);
        }
    }
}
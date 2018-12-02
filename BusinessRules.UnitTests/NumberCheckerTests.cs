using System;
using Common;
using NFluent;
using NUnit.Framework;

namespace BusinessRules.UnitTests {
    public class NumberCheckerTests {
        private static readonly INumberChecker _numberChecker =
            new NumberChecker(Resources.CorrectNumber);
        
        private static int numberToCheck;
        private static string expectedCheckResult;
        private static string actualCheckResult;
        
        [Test]
        public void NumberChecker_LowerThanCorrectNumber_TooLowMessage() {
            // Arrange
            expectedCheckResult = Resources.TooLowMessage;
            numberToCheck = Resources.CorrectNumber - 1;
            
            // Act
            actualCheckResult = _numberChecker.CheckNumber(numberToCheck);

            // Assert
            Check.That(actualCheckResult).Equals(expectedCheckResult);
        }

        [Test]
        public void NumberChecker_HigherThanCorrectNumber_TooHighMessage() {
            // Arrange
            expectedCheckResult = Resources.TooHighMessage;
            numberToCheck = Resources.CorrectNumber + 1;

            // Act
            actualCheckResult = _numberChecker.CheckNumber(numberToCheck);

            // Assert
            Check.That(actualCheckResult).Equals(expectedCheckResult);
        }
        
        [Test]
        public void NumberChecker_CorrectNumber_CorrectMessage() {
            // Arrange
            expectedCheckResult = Resources.CorrectMessage;
            numberToCheck = Resources.CorrectNumber;

            // Act
            actualCheckResult = _numberChecker.CheckNumber(numberToCheck);

            // Assert
            Check.That(actualCheckResult).Equals(expectedCheckResult);
        }
        
        [TestCase(Resources.LowerBound - 1)]
        [TestCase(Resources.UpperBound + 1)]
        [TestCase(int.MaxValue)]
        [TestCase(int.MinValue)]
        public void NumberChecker_OutOfBoundsNumber_Exception(int number) {
            // Arrange
            void NumberCheckCode() => _numberChecker.CheckNumber(number);

            // Act && Assert
            Check.ThatCode(NumberCheckCode).Throws<ArgumentOutOfRangeException>();
        }
    }
}
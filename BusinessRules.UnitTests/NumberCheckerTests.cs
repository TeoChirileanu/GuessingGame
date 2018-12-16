using System;
using Common;
using NFluent;
using NUnit.Framework;

namespace BusinessRules.UnitTests {
    public class NumberCheckerTests {
        private static readonly INumberChecker NumberChecker =
            new NumberChecker(Resources.CorrectNumber);

        private static int _numberToCheck;
        private static string _expectedCheckResult;
        private static string _actualCheckResult;

        [Test]
        public void NumberChecker_LowerThanCorrectNumber_TooLowMessage() {
            // Arrange
            _expectedCheckResult = Resources.TooLowMessage;
            _numberToCheck = Resources.CorrectNumber - 1;

            // Act
            _actualCheckResult = NumberChecker.CheckNumber(_numberToCheck);

            // Assert
            Check.That(_actualCheckResult).Equals(_expectedCheckResult);
        }

        [Test]
        public void NumberChecker_HigherThanCorrectNumber_TooHighMessage() {
            // Arrange
            _expectedCheckResult = Resources.TooHighMessage;
            _numberToCheck = Resources.CorrectNumber + 1;

            // Act
            _actualCheckResult = NumberChecker.CheckNumber(_numberToCheck);

            // Assert
            Check.That(_actualCheckResult).Equals(_expectedCheckResult);
        }

        [Test]
        public void NumberChecker_CorrectNumber_CorrectMessage() {
            // Arrange
            _expectedCheckResult = Resources.CorrectMessage;
            _numberToCheck = Resources.CorrectNumber;

            // Act
            _actualCheckResult = NumberChecker.CheckNumber(_numberToCheck);

            // Assert
            Check.That(_actualCheckResult).Equals(_expectedCheckResult);
        }

        [TestCase(Resources.LowerBound - 1)]
        [TestCase(Resources.UpperBound + 1)]
        [TestCase(int.MaxValue)]
        [TestCase(int.MinValue)]
        public void NumberChecker_OutOfBoundsNumber_Exception(int number) {
            // Arrange
            INumberChecker defaultNumberChecker = new NumberChecker();
            void NumberCheckCode() => defaultNumberChecker.CheckNumber(number);

            // Act && Assert
            Check.ThatCode(NumberCheckCode).Throws<ArgumentOutOfRangeException>();
        }
    }
}
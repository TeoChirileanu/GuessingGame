using System;
using System.Threading.Tasks;
using GuessingGame.BusinessRules;
using GuessingGame.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace BusinessRules.UnitTests {
    [TestClass]
    public class NumberCheckerTests {
        private static readonly INumberChecker NumberChecker =
            new NumberChecker(Resources.CorrectNumber);

        private static int _numberToCheck;
        private static string _expectedCheckResult;
        private static string _actualCheckResult;

        [TestMethod]
        public async Task NumberChecker_LowerThanCorrectNumber_TooLowMessage() {
            // Arrange
            _expectedCheckResult = Resources.TooLowMessage;
            _numberToCheck = Resources.CorrectNumber - 1;

            // Act
            _actualCheckResult = await NumberChecker.CheckNumber(_numberToCheck);

            // Assert
            Check.That(_actualCheckResult).Equals(_expectedCheckResult);
        }

        [TestMethod]
        public async Task NumberChecker_HigherThanCorrectNumber_TooHighMessage() {
            // Arrange
            _expectedCheckResult = Resources.TooHighMessage;
            _numberToCheck = Resources.CorrectNumber + 1;

            // Act
            _actualCheckResult = await NumberChecker.CheckNumber(_numberToCheck);

            // Assert
            Check.That(_actualCheckResult).Equals(_expectedCheckResult);
        }

        [TestMethod]
        public async Task NumberChecker_CorrectNumber_CorrectMessage() {
            // Arrange
            _expectedCheckResult = Resources.CorrectMessage;
            _numberToCheck = Resources.CorrectNumber;

            // Act
            _actualCheckResult = await NumberChecker.CheckNumber(_numberToCheck);

            // Assert
            Check.That(_actualCheckResult).Equals(_expectedCheckResult);
        }

        [DataRow(Resources.LowerBound - 1)]
        [DataRow(Resources.UpperBound + 1)]
        [DataRow(int.MaxValue)]
        [DataRow(int.MinValue)]
        [TestMethod]
        public void NumberChecker_OutOfBoundsNumber_Exception(int number) {
            // Arrange
            INumberChecker defaultNumberChecker = new NumberChecker();
            async Task NumberCheckCode() => await defaultNumberChecker.CheckNumber(number);

            // Act && Assert
            Check.ThatCode(NumberCheckCode).Throws<ArgumentOutOfRangeException>();
        }
    }
}
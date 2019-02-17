using GuessingGame.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace Common.UnitTests {
    [TestClass]
    public class NumberValidatorTests {
        private static string NumberAsString { get; set; }

        [DataRow(null)]
        [DataRow("")]
        [DataRow("\r\n\t")]
        [TestMethod]
        public void GetIntValue_NullOrWhitespace_null(string value) {
            // Arrange
            NumberAsString = value;

            // Act
            int? actualResult = NumberAsString.GetIntValue();

            // Assert
            Check.That(actualResult).Equals(null);
        }

        [TestMethod]
        public void GetIntValue_ValidNumber_Number() {
            // Arrange
            NumberAsString = "123";

            // Act
            int? actualResult = NumberAsString.GetIntValue();

            // Assert
            Check.That(actualResult).Equals(123);
        }

        [TestMethod]
        public void GetIntValue_InvalidNumber_Null() {
            // Arrange
            NumberAsString = "asd";

            // Act
            int? actualResult = NumberAsString.GetIntValue();

            // Assert
            Check.That(actualResult).Equals(null);
        }
    }
}
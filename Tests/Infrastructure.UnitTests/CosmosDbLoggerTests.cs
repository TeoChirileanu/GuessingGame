using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace Infrastructure.UnitTests {
    [TestClass, Ignore("Need to mock local dev server")]
    public class CosmosDbLoggerTests {
        private const string Message = "test message";

        private readonly CosmosDbLogger _logger = new CosmosDbLogger();

        [TestCleanup]
        public void CleanUp() {
            _logger.ClearLog();
        }

        [TestMethod]
        public void ShouldCorrectlyLogMessage() {
            // Arrange
            const string expectedMessage = Message;

            // Act
            _logger.Log(Message);
            var actualMessage = _logger.GetLoggedGuesses();

            // Assert
            Check.That(actualMessage).Equals(expectedMessage);
        }
    }
}
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace Infrastructure.UnitTests {
    [TestClass, Ignore("Ensure Cosmos Service is up and running")]
    public class CosmosDbLoggerTests {
        private const string Message = "test message";

        private readonly CosmosDbLogger _logger = new CosmosDbLogger();

        [TestCleanup]
        public async Task CleanUp() {
            await _logger.ClearLog();
        }

        [TestMethod]
        public async Task ShouldCorrectlyLogMessage() {
            // Arrange
            const string expectedMessage = Message;

            // Act
            await _logger.Log(Message);
            var actualMessage = await _logger.GetLoggedGuesses();

            // Assert
            Check.That(actualMessage).Equals(expectedMessage);
        }
    }
}
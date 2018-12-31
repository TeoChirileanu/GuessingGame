using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace Infrastructure.UnitTests {
    [TestClass]
    public class LiteDbLoggerTests {
        private const string Message = "test message";
        private const string TestDatabaseName = "test.litedb";
        private const string TestCollectionName = "TestCollection";

        private readonly LiteDbLogger _liteDbLogger = new LiteDbLogger {
            DatabaseName = TestDatabaseName,
            CollectionName = TestCollectionName
        };

        [TestCleanup]
        public async Task CleanUp() {
            await _liteDbLogger.ClearLog();
        }

        [TestMethod]
        public async Task ShouldCorrectlyLogMessage() {
            // Arrange
            const string expectedMessage = Message;

            // Act
            await _liteDbLogger.Log(Message);
            var actualMessage = await _liteDbLogger.GetLoggedGuesses();

            // Assert
            Check.That(actualMessage).Equals(expectedMessage);
        }
    }
}
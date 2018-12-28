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
        public void CleanUp() {
            _liteDbLogger.ClearLog();
        }

        [TestMethod]
        public void ShouldCorrectlyLogMessage() {
            // Arrange
            const string expectedMessage = Message;

            // Act
            _liteDbLogger.Log(Message);
            var actualMessage = _liteDbLogger.GetLoggedGuesses();

            // Assert
            Check.That(actualMessage).Equals(expectedMessage);
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace Infrastructure.UnitTests {
    [TestClass]
    public class SqliteLoggerTests {
        private const string Message = "test message";
        private const string TestDatabaseName = "sqlitetest";

        private readonly SqlLiteLogger _sqlLiteLogger = new SqlLiteLogger(TestDatabaseName);

        [TestCleanup]
        public void CleanUp() {
            _sqlLiteLogger.ClearLog();
        }

        [TestMethod]
        public void ShouldCorrectlyLogMessage() {
            // Arrange
            const string expectedMessage = Message;

            // Act
            _sqlLiteLogger.Log(Message);
            var actualMessage = _sqlLiteLogger.GetLoggedGuesses();

            // Assert
            Check.That(actualMessage).Equals(expectedMessage);
        }
    }
}
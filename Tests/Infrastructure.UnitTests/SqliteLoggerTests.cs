using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace Infrastructure.UnitTests {
    [TestClass]
    public class SqliteLoggerTests {
        private const string Message = "test message";
        private const string TestDatabaseName = "sqlitetest";

        private readonly SqlLiteLogger _sqlLiteLogger = new SqlLiteLogger(TestDatabaseName);

        [TestCleanup]
        public async Task CleanUp() {
            await _sqlLiteLogger.ClearLog();
        }

        [TestMethod]
        public async Task ShouldCorrectlyLogMessage() {
            // Arrange
            const string expectedMessage = Message;

            // Act
            await _sqlLiteLogger.Log(Message);
            var actualMessage = await _sqlLiteLogger.GetLoggedGuesses();

            // Assert
            Check.That(actualMessage).Equals(expectedMessage);
        }
    }
}
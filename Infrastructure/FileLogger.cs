using System.IO;
using System.Threading.Tasks;
using GuessingGame.UseCases;

namespace GuessingGame.Infrastructure {
    public class FileLogger : ILogger {
        private const string FileName = "log.txt";

        public async Task Log(string message) {
            using (var stream = new StreamWriter(FileName, true)) {
                await stream.WriteLineAsync(message);
            }
        }

        public async Task<string> GetLoggedGuesses() {
            using (var stream = new StreamReader(FileName)) {
                return await stream.ReadToEndAsync();
            }
        }

        public async Task ClearLog() {
            using (var stream = new StreamWriter(FileName)) {
                await stream.WriteAsync(string.Empty);
            }
        }
    }
}
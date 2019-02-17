using System.Text;
using System.Threading.Tasks;
using GuessingGame.UseCases;

namespace GuessingGame.Infrastructure {
    public class StringBuilderLogger : ILogger {
        private static readonly StringBuilder StringBuilder = new StringBuilder();

        public async Task ClearLog() {
            StringBuilder.Clear();
            await Task.CompletedTask;
        }

        public async Task Log(string message) {
            StringBuilder.AppendLine(message);
            await Task.CompletedTask;
        }

        public async Task<string> GetLoggedGuesses() {
            var result = StringBuilder.ToString();
            return await Task.FromResult(result);
        }
    }
}
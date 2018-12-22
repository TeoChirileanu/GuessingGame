using System.Text;
using UseCases;

namespace Infrastructure {
    public class StringBuilderLogger : ILogger {
        private static readonly StringBuilder StringBuilder = new StringBuilder();

        public void ClearLog() => StringBuilder.Clear();

        public void Log(string message) => StringBuilder.AppendLine(message);

        public string GetLoggedGuesses() => StringBuilder.ToString();
    }
}
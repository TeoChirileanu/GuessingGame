using System;
using System.Text;
using UseCases;

namespace Infrastructure {
    public class StringBuilderLogger : ILogger, IDisposable {
        private static readonly StringBuilder StringBuilder = new StringBuilder();

        public void Dispose() => StringBuilder.Clear();

        public void Log(string message) => StringBuilder.AppendLine(message);

        public string GetLoggedGuesses() => StringBuilder.ToString();
    }
}
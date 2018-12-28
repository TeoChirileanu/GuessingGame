using System.IO;
using UseCases;

namespace Infrastructure {
    public class FileLogger : ILogger {
        private const string FileName = "log.txt";

        public void Log(string message) {
            using (var stream = new StreamWriter(FileName, true)) {
                stream.WriteLine(message);
            }
        }

        public string GetLoggedGuesses() {
            using (var stream = new StreamReader(FileName)) {
                return stream.ReadLine();
            }
        }

        public void ClearLog() {
            using (var stream = new StreamWriter(FileName)) {
                stream.Write(string.Empty);
            }
        }
    }
}
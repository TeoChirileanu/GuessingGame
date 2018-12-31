using System;
using System.Threading.Tasks;
using UseCases;

namespace Infrastructure {
    public class StdoutGuessResultDeliverer : IDeliverer {
        public async Task Deliver(string guessResult) {
            Console.WriteLine(guessResult);
            await Task.CompletedTask;
        }
    }
}
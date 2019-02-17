using System;
using System.Threading.Tasks;
using GuessingGame.UseCases;

namespace GuessingGame.Infrastructure {
    public class StdoutGuessResultDeliverer : IDeliverer {
        public async Task Deliver(string guessResult) {
            Console.WriteLine(guessResult);
            await Task.CompletedTask;
        }
    }
}
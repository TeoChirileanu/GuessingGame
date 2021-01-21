using System;
using System.Threading.Tasks;
using GuessingGame.UseCases;

namespace GuessingGame.Infrastructure {
    public class StdoutNumberDeliverer : INumberDeliverer {
        public async Task Deliver(string guessResult) {
            Console.WriteLine(guessResult);
            await Task.CompletedTask;
        }
    }
}
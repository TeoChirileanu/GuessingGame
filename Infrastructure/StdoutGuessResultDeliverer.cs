using System;
using UseCases;

namespace Infrastructure {
    public class StdoutGuessResultDeliverer : IDeliverer {
        public void Deliver(string guessResult) => Console.WriteLine(guessResult);
    }
}
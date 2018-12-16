using System;
using UseCases;

namespace Infrastructure {
    public class StdoutGuessResultDeliverer : IGuessResultDeliverer {
        public void DeliverGuessResult(string guessResult) => Console.WriteLine(guessResult);
    }
}
using System;
using BusinessRules;
using Common;
using Infrastructure;
using UseCases;

namespace Gui.ConsoleApplication {
    public static class ConsoleApplication {
        public static void Main() {
            IGuessedNumberGetter getter = new StdinGuessedNumberGetter();
            ILogger logger = new CosmosDbLogger();
            INumberChecker checker = new NumberChecker(50);
            IDeliverer deliverer = new StdoutGuessResultDeliverer();

            var guessFacade = new GuessFacade(getter, checker, logger, deliverer);
            try {
                Run(guessFacade);
            }
            catch (Exception e) {
                Console.WriteLine($"Oops, something bad happened!\n{e}");
            }
        }

        private static void Run(IGuessFacade guessFacade) {
            var gameOver = false;
            do {
                var guessedNumber = guessFacade.GetGuessedNumber();
                var guessResult = guessFacade.CheckGuessedNumber(guessedNumber);
                guessFacade.DeliverGuessResult(guessResult);
                if (guessResult.Equals(Resources.CorrectMessage)) gameOver = true;
            } while (!gameOver);

            guessFacade.DeliverLoggedGuesses();
        }
    }
}
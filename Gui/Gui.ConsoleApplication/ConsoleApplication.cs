using System;
using System.Threading.Tasks;
using BusinessRules;
using Common;
using Infrastructure;
using UseCases;

namespace Gui.ConsoleApplication {
    public static class ConsoleApplication {
        public static async Task Main() {
            IGuessedNumberGetter getter = new StdinGuessedNumberGetter();
            ILogger logger = new CosmosDbLogger();
            INumberChecker checker = new NumberChecker(50);
            IDeliverer deliverer = new StdoutGuessResultDeliverer();

            var guessFacade = new GuessFacade(getter, checker, logger, deliverer);
            try {
                await Run(guessFacade);
            }
            catch (Exception e) {
                Console.WriteLine($"Oops, something bad happened!\n{e}");
            }
        }

        private static async Task Run(IGuessFacade guessFacade) {
            var gameOver = false;
            do {
                var guessedNumber = await guessFacade.GetGuessedNumber();
                var guessResult = await guessFacade.CheckGuessedNumber(guessedNumber);
                await guessFacade.DeliverGuessResult(guessResult);
                if (guessResult.Equals(Resources.CorrectMessage)) gameOver = true;
            } while (!gameOver);

            await guessFacade.DeliverLoggedGuesses();
        }
    }
}
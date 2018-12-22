using BusinessRules;
using Common;
using Infrastructure;
using UseCases;

namespace Gui.ConsoleApplication {
    public static class ConsoleApplication {
        public static void Main() {
            IGuessedNumberGetter numberGetter = new StdinGuessedNumberGetter();
            ILogger logger = new StringBuilderLogger();
            INumberChecker numberChecker = new NumberChecker();
            IDeliverer deliverer = new StdoutGuessResultDeliverer();

            var guessFacade = new GuessFacade(numberGetter, numberChecker, logger, deliverer);
            Run(guessFacade);
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
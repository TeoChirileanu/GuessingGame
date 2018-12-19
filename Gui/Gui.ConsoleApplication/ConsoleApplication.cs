using System;
using BusinessRules;
using Common;
using Infrastructure;
using UseCases;

namespace Gui.ConsoleApplication
{
    public static class ConsoleApplication
    {
        public static void Main() {
            IGuessedNumberGetter stdinGuessedNumberGetter = new StdinGuessedNumberGetter();
            INumberChecker numberChecker = new NumberChecker();
            IGuessResultDeliverer guessResultDeliverer = new StdoutGuessResultDeliverer();
            
            var guessFacade = new GuessFacade(stdinGuessedNumberGetter, numberChecker, guessResultDeliverer);
            Run(guessFacade);
        }

        private static void Run(IGuessFacade guessFacade)
        {
            var gameOver = false;
            do
            {
                int? guessedNumber = guessFacade.GetGuessedNumber();
                if (guessedNumber == null) continue;
                var guessResult = guessFacade.CheckGuessedNumber(guessedNumber.Value);
                if (guessResult.Equals(Resources.CorrectMessage)) gameOver = true;
                guessFacade.DeliverGuessResult(guessResult);
            } while (!gameOver);
        }
    }
}

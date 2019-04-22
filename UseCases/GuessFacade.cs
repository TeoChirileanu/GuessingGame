using System;
using System.Threading.Tasks;
using GuessingGame.BusinessRules;
using GuessingGame.Common;

namespace GuessingGame.UseCases {
    public class GuessFacade : IGuessFacade {
        public IGuessedNumberGetter GuessedNumberGetter { private get; set; }
        public IDeliverer Deliverer { private get; set; }
        public ILogger Logger { private get; set; }
        public INumberChecker NumberChecker { private get; set; }

        public async Task<int> GetGuessedNumber() {
            if (GuessedNumberGetter is null)
                throw new ArgumentNullException(Resources.NoGetterProvided);
            int? number;
            var numberHasValue = false;
            do {
                number = await GuessedNumberGetter.GetGuessedNumber();
                if (number.HasValue) numberHasValue = true;
            } while (!numberHasValue);

            return number.Value;
        }

        public async Task<string> CheckGuessedNumber(int guessedNumber) {
            if (Logger is null) throw new ArgumentNullException(Resources.NoLoggerProvided);
            if (NumberChecker is null)
                throw new ArgumentNullException(Resources.NoNumberCheckerProvided);

            await Logger.Log(string.Format(Resources.CheckingNumberMessage, guessedNumber));
            var guessResult = await NumberChecker.CheckNumber(guessedNumber);
            await Logger.Log(guessResult);
            return guessResult;
        }

        public async Task DeliverGuessResult(string guessResult) {
            if (Deliverer is null) throw new ArgumentNullException(Resources.NoDelivererProvided);
            var message = $"The result of your guess:\n{guessResult}\n";
            await Deliverer.Deliver(message);
        }

        public async Task DeliverLoggedGuesses() {
            if (Logger is null) throw new ArgumentNullException(Resources.NoLoggerProvided);
            if (Deliverer is null) throw new ArgumentNullException(Resources.NoDelivererProvided);

            var previousAttempts = await Logger.GetLoggedGuesses();
            var message = $"Here are your guesses so far:\n{previousAttempts}\n";
            await Deliverer.Deliver(message);
            await Logger.ClearLog();
        }
    }
}
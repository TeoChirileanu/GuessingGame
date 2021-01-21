using System.Threading.Tasks;
using GuessingGame.BusinessRules;
using GuessingGame.Common;

namespace GuessingGame.UseCases
{
    public class GuessFacade : IGuessFacade
    {
        public INumberGetter NumberGetter { private get; set; }
        public INumberChecker NumberChecker { private get; set; }
        public INumberDeliverer NumberDeliverer { private get; set; }

        public ILogger Logger { private get; set; }

        public async Task<int> GetGuessedNumber()
        {
            int? number;
            var numberHasValue = false;
            do
            {
                number = await NumberGetter.GetGuessedNumber();
                if (number.HasValue) numberHasValue = true;
            } while (!numberHasValue);

            return number.Value;
        }

        public async Task<string> CheckGuessedNumber(int guessedNumber)
        {
            await Logger.Log(string.Format(Resources.CheckingNumberMessage,
                guessedNumber));
            var guessResult = await NumberChecker.CheckNumber(guessedNumber);
            await Logger.Log(guessResult);
            return guessResult;
        }

        public async Task DeliverGuessResult(string guessResult)
        {
            await NumberDeliverer.Deliver($"The result of your guess:\n{guessResult}\n");
        }

        public async Task DeliverLoggedGuesses()
        {
            var previousAttempts = await Logger.GetLoggedGuesses();
            var message = $"Here are your guesses so far:\n{previousAttempts}\n";
            await NumberDeliverer.Deliver(message);
            await Logger.ClearLog();
        }
    }
}
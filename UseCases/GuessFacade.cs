using System.Threading.Tasks;
using BusinessRules;
using Common;

namespace UseCases {
    public class GuessFacade : IGuessFacade {
        private readonly IGuessedNumberGetter _guessedNumberGetter;
        private readonly IDeliverer _guessResultDeliverer;
        private readonly ILogger _logger;
        private readonly INumberChecker _numberChecker;

        public GuessFacade(IGuessedNumberGetter guessedNumberGetter, INumberChecker numberChecker,
            ILogger logger, IDeliverer deliverer) {
            _guessedNumberGetter = guessedNumberGetter;
            _numberChecker = numberChecker;
            _guessResultDeliverer = deliverer;
            _logger = logger;
        }

        public async Task<int> GetGuessedNumber() {
            int? number;
            var numberHasValue = false;
            do {
                number = await _guessedNumberGetter.GetGuessedNumber();
                if (number.HasValue) numberHasValue = true;
            } while (!numberHasValue);

            return number.Value;
        }

        public async Task<string> CheckGuessedNumber(int guessedNumber) {
            var checkingNumberMessageFormat =
                string.Format(Resources.CheckingNumberMessage, guessedNumber);
            await _logger.Log(checkingNumberMessageFormat);
            var guessResult = await _numberChecker.CheckNumber(guessedNumber);
            await _logger.Log(guessResult);
            return guessResult;
        }

        public async Task DeliverGuessResult(string guessResult) {
            var message = $"The result of your guess:\n{guessResult}\n";
            await _guessResultDeliverer.Deliver(message);
        }


        public async Task DeliverLoggedGuesses() {
            var previousAttempts = await _logger.GetLoggedGuesses();
            var message = $"Here are your guesses so far:\n{previousAttempts}\n";
            await _guessResultDeliverer.Deliver(message);
            await _logger.ClearLog();
        }
    }
}
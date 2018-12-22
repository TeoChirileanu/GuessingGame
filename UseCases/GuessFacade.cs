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

        public int GetGuessedNumber() {
            int? number;
            var numberHasValue = false;
            do {
                number = _guessedNumberGetter.GetGuessedNumber();
                if (number.HasValue) numberHasValue = true;
            } while (!numberHasValue);

            return number.Value;
        }

        public string CheckGuessedNumber(int guessedNumber) {
            var checkingNumberMessageFormat =
                string.Format(Resources.CheckingNumberMessage, guessedNumber);
            _logger.Log(checkingNumberMessageFormat);
            var guessResult = _numberChecker.CheckNumber(guessedNumber);
            _logger.Log(guessResult);
            return guessResult;
        }

        public void DeliverGuessResult(string guessResult) {
            var message = $"Here is the result of your guess:\n{guessResult}\n";
            _guessResultDeliverer.Deliver(message);
        }


        public void DeliverLoggedGuesses() {
            var previousAttempts = _logger.GetLoggedGuesses();
            var message = $"Here are your previous attempts:\n{previousAttempts}\n";
            _guessResultDeliverer.Deliver(message);
        }
    }
}
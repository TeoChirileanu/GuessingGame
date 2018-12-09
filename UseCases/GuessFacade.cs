using BusinessRules;

namespace UseCases {
    public class GuessFacade : IGuessFacade {
        private readonly IGuessedNumberGetter _guessedNumberGetter;
        private readonly IGuessResultDeliverer _guessResultDeliverer;
        private readonly INumberChecker _numberChecker;

        public GuessFacade(IGuessedNumberGetter guessedNumberGetter,
            INumberChecker numberChecker, IGuessResultDeliverer guessResultDeliverer) {
            _guessedNumberGetter = guessedNumberGetter;
            _numberChecker = numberChecker;
            _guessResultDeliverer = guessResultDeliverer;
        }

        public int? GetGuessedNumber() => _guessedNumberGetter.GetGuessedNumber();

        public string CheckGuessedNumber(int guessedNumber) =>
            _numberChecker.CheckNumber(guessedNumber);

        public void DeliverGuessResult(string guessResult) =>
            _guessResultDeliverer.DeliverGuessResult(guessResult);
    }
}
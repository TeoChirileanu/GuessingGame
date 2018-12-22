namespace UseCases {
    public interface IGuessFacade {
        int GetGuessedNumber();
        string CheckGuessedNumber(int guessedNumber);
        void DeliverGuessResult(string guessResult);
        void DeliverLoggedGuesses();
    }
}
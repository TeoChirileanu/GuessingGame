namespace Common {
    public static class Resources {
        public const int LowerBound = 0;
        public const int UpperBound = 100;
        public const int CorrectNumber = 50;

        public static readonly string TooLowMessage = "Too Low!";
        public static readonly string TooHighMessage = "Too High!";
        public static readonly string CorrectMessage = "Correct!";

        public static readonly string NumberOutOfBoundsMessage =
            $"Number is out of range. It should be between {LowerBound} and {UpperBound}";

        public static readonly string AskUserForNumberMessage =
            $"Please enter a number between {LowerBound} and {UpperBound}";
    }
}
using System;

namespace GuessingGame.Common {
    public static partial class Resources {
        public const int LowerBound = 0;
        public const int UpperBound = 100;
        public const int CorrectNumber = 50;

        public static readonly string TooLowMessage = "Too Low!";
        public static readonly string TooHighMessage = "Too High!";
        public static readonly string CorrectMessage = "Correct!";
        public static readonly string CheckingNumberMessage = "Checking {0}...";

        public static readonly string NumberOutOfBoundsMessage =
            $"Number is out of range. It should be between {LowerBound} and {UpperBound}";

        public static readonly string AskUserForNumberMessage =
            $"Please enter a number between {LowerBound} and {UpperBound}";

        public static readonly string NoGetterProvided = "No Guessed Number Getter Provided!";

        public static readonly string NoLoggerProvided = "No Logger Provided!";
        public static readonly string NoNumberCheckerProvided = "No Number Checker Provided!";
        public static readonly string NoDelivererProvided = "No Deliverer Provided!";

        public static int GetRandomNumber() => new Random().Next(LowerBound, UpperBound);
    }

    public static partial class Resources {
        private const string LocalHostAddress = "https://localhost";
        private const int CosmosDbPort = 8081;

        public static readonly string LocalHostUri = $"{LocalHostAddress}:{CosmosDbPort}";
    }
}
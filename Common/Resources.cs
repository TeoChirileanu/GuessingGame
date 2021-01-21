using System;

namespace GuessingGame.Common {
    public static partial class Resources {
        public const int LowerBound = 0;
        public const int UpperBound = 100;
        public const int CorrectNumber = 50;

        public const string TooLowMessage = "Too Low!";
        public const string TooHighMessage = "Too High!";
        public const string CorrectMessage = "Correct!";
        public const string CheckingNumberMessage = "Checking {0}...";

        public const string InvalidNumberMessage = "That is not a valid number!";

        public static readonly string NumberOutOfBoundsMessage =
            $"Number is out of range. It should be between {LowerBound} and {UpperBound}";

        public static readonly string AskUserForNumberMessage =
            $"Please enter a number between {LowerBound} and {UpperBound}";

        public static int GetRandomNumber() => new Random().Next(LowerBound, UpperBound);
    }

    public static partial class Resources {
        private const string LocalHostAddress = "https://localhost";
        private const int CosmosDbPort = 8081;

        public static readonly string LocalHostUri = $"{LocalHostAddress}:{CosmosDbPort}";
    }
}
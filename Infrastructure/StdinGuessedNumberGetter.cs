using System;
using Common;
using UseCases;

namespace Infrastructure {
    public class StdinGuessedNumberGetter : IGuessedNumberGetter {
        public int? GetGuessedNumber() {
            bool parsedSuccessfully;
            int parsedNumber;
            do {
                var messageToUser = string.Format(Resources.AskUserForNumberMessage);
                Console.WriteLine(messageToUser);
                var userInput = Console.ReadLine();
                parsedSuccessfully = int.TryParse(userInput, out parsedNumber);
            } while (!parsedSuccessfully);

            var isNumberWithinBounds = parsedNumber > Resources.LowerBound &&
                                       parsedNumber < Resources.UpperBound;

            if (!isNumberWithinBounds) return null;

            return parsedNumber;
        }
    }
}
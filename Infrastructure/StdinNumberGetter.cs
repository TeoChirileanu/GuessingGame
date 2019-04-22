using System;
using System.Threading.Tasks;
using GuessingGame.Common;
using GuessingGame.UseCases;

namespace GuessingGame.Infrastructure {
    public class StdinNumberGetter : INumberGetter {
        public async Task<int?> GetGuessedNumber() {
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

            return await Task.FromResult(parsedNumber);
        }
    }
}
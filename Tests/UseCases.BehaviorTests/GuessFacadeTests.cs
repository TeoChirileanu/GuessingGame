using System.Threading.Tasks;
using GuessingGame.BusinessRules;
using GuessingGame.Common;
using GuessingGame.UseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace UseCases.BehaviorTests
{
    [TestClass]
    public class GuessFacadeTests
    {
        private readonly INumberChecker _checker = Substitute.For<INumberChecker>();
        private readonly INumberDeliverer _deliverer = Substitute.For<INumberDeliverer>();
        private readonly INumberGetter _getter = Substitute.For<INumberGetter>();
        private readonly ILogger _logger = Substitute.For<ILogger>();

        [TestMethod]
        public async Task ShouldCallAppropriateCollaborators()
        {
            // Arrange
            _getter.GetGuessedNumber().Returns(Resources.CorrectNumber);
            IGuessFacade fakeGuessFacade = new GuessFacade
            {
                NumberGetter = _getter,
                NumberChecker = _checker,
                NumberDeliverer = _deliverer,
                Logger = _logger
            };

            // Act
            var guessedNumber = await fakeGuessFacade.GetGuessedNumber();
            var guessResult = await fakeGuessFacade.CheckGuessedNumber(guessedNumber);
            await fakeGuessFacade.DeliverGuessResult(guessResult);
            await fakeGuessFacade.DeliverLoggedGuesses();

            // Assert
            await _getter.Received().GetGuessedNumber();
            await _checker.Received().CheckNumber(Arg.Any<int>());
            await _deliverer.Received().Deliver(Arg.Any<string>());
            await _logger.Received().Log(Arg.Any<string>());
            await _logger.Received().GetLoggedGuesses();
            await _logger.Received().ClearLog();
        }
    }
}
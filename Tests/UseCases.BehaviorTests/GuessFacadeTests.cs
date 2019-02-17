using System.Threading.Tasks;
using GuessingGame.BusinessRules;
using GuessingGame.Common;
using GuessingGame.UseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace UseCases.BehaviorTests {
    [TestClass]
    public class GuessFacadeTests {
        private static readonly IGuessedNumberGetter FakeGuessedNumberGetter =
            Substitute.For<IGuessedNumberGetter>();

        private static readonly INumberChecker FakeNumberChecker =
            Substitute.For<INumberChecker>();

        private static readonly IDeliverer FakeGuessResultDeliverer =
            Substitute.For<IDeliverer>();

        private static readonly ILogger FakeLogger =
            Substitute.For<ILogger>();

        [TestMethod]
        public async Task ShouldCallAppropriateCollaborators() {
            // Arrange
            FakeGuessedNumberGetter.GetGuessedNumber().Returns(Resources.CorrectNumber);
            IGuessFacade fakeGuessFacade = new GuessFacade {
                GuessedNumberGetter = FakeGuessedNumberGetter,
                Deliverer = FakeGuessResultDeliverer,
                Logger = FakeLogger,
                NumberChecker = FakeNumberChecker
            };

            // Act
            var guessedNumber = await fakeGuessFacade.GetGuessedNumber();
            var guessResult = await fakeGuessFacade.CheckGuessedNumber(guessedNumber);
            await fakeGuessFacade.DeliverGuessResult(guessResult);
            await fakeGuessFacade.DeliverLoggedGuesses();

            // Assert
            await FakeGuessedNumberGetter.Received().GetGuessedNumber();
            await FakeNumberChecker.Received().CheckNumber(Arg.Any<int>());
            await FakeGuessResultDeliverer.Received().Deliver(Arg.Any<string>());
            await FakeLogger.Received().Log(Arg.Any<string>());
            await FakeLogger.Received().GetLoggedGuesses();
            await FakeLogger.Received().ClearLog();
        }
    }
}
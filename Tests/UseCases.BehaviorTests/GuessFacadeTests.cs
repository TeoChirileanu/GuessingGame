using BusinessRules;
using Common;
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
        public void ShouldCallAppropriateCollaborators() {
            // Arrange
            FakeGuessedNumberGetter.GetGuessedNumber().Returns(Resources.CorrectNumber);
            IGuessFacade fakeGuessFacade = new GuessFacade(
                FakeGuessedNumberGetter, FakeNumberChecker, FakeLogger, FakeGuessResultDeliverer);

            // Act
            var guessedNumber = fakeGuessFacade.GetGuessedNumber();
            var guessResult = fakeGuessFacade.CheckGuessedNumber(guessedNumber);
            fakeGuessFacade.DeliverGuessResult(guessResult);
            fakeGuessFacade.DeliverLoggedGuesses();

            // Assert
            FakeGuessedNumberGetter.Received().GetGuessedNumber();
            FakeNumberChecker.Received().CheckNumber(Arg.Any<int>());
            FakeGuessResultDeliverer.Received().Deliver(Arg.Any<string>());
            FakeLogger.Received().Log(Arg.Any<string>());
            FakeLogger.Received().GetLoggedGuesses();
            FakeLogger.Received().ClearLog();
        }
    }
}
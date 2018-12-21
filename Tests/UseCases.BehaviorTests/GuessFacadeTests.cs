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

        private static readonly IGuessResultDeliverer FakeGuessResultDeliverer =
            Substitute.For<IGuessResultDeliverer>();

        [TestMethod]
        public void ShouldCallAppropriateCollaborators() {
            // Arrange
            FakeGuessedNumberGetter.GetGuessedNumber().Returns(Resources.CorrectNumber);
            IGuessFacade fakeGuessFacade = new GuessFacade(FakeGuessedNumberGetter,
                FakeNumberChecker, FakeGuessResultDeliverer);

            // Act
            var guessedNumber = fakeGuessFacade.GetGuessedNumber().GetValueOrDefault();
            var guessResult = fakeGuessFacade.CheckGuessedNumber(guessedNumber);
            fakeGuessFacade.DeliverGuessResult(guessResult);

            // Assert
            FakeGuessedNumberGetter.Received().GetGuessedNumber();
            FakeNumberChecker.Received().CheckNumber(Resources.CorrectNumber);
            FakeGuessResultDeliverer.DeliverGuessResult(Resources.CorrectMessage);
        }
    }
}
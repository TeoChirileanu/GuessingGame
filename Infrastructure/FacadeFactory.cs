using GuessingGame.BusinessRules;
using GuessingGame.UseCases;

namespace GuessingGame.Infrastructure
{
    public static class FacadeFactory
    {
        private static readonly INumberGetter NumberGetter = new StdinNumberGetter();
        private static readonly INumberChecker NumberChecker = new NumberChecker();
        private static readonly INumberDeliverer NumberDeliverer = new StdoutNumberDeliverer();

        public static IGuessFacade MakeStandardFacadeWithInMemoryLogger()
        {
            return new GuessFacade
            {
                NumberGetter = NumberGetter,
                NumberChecker = NumberChecker,
                NumberDeliverer = NumberDeliverer,
                Logger = new StringBuilderLogger()
            };
        }
    }
}
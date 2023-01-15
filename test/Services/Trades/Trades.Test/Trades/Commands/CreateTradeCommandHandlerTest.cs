using EventBus.RabbitMQ.Trades.Notificaitons;
using Microsoft.EntityFrameworkCore;
using Moq;
using Trades.Application.Common.Interfaces;
using Trades.Application.PublishTrades.Interfaces;
using Trades.Application.Trades.Commands.CreateTrade;
using Trades.Domain.Entity;

namespace Trades.Test.Trades.Commands
{
    [TestFixture]
    public class CreateTradeCommandHandlerTest
    {

        private  Mock<ITradeApplicationDbContext> _mockTradeRepo;
        private  Mock<IPublishTrade> _mockIPublishTrade;

       
        [SetUp]
        public void Setup()
        {
            _mockTradeRepo = new Mock<ITradeApplicationDbContext>();
            _mockIPublishTrade = new Mock<IPublishTrade>();

        }
     
        [Test]
        public async Task Handle_ShouldReturnSuccessResult()
        {
            var mockSet = new Mock<DbSet<Trade>>();
            _mockTradeRepo.Setup(m => m.Trades).Returns(mockSet.Object);
            var command = new CreateTradeCommand
            {
                CommoditiesIdentifier = "AU",
                CounterpartiesIdentifier = "1",
                LocationIdentifier = "KOL",
                Price = 100,
                Quantity = 100,
                Side = (Domain.Common.Side)2
            };
            TradesMessage notificationMessage = It.IsAny<TradesMessage>();
            var handler = new CreateTradeCommandHandler(_mockTradeRepo.Object, _mockIPublishTrade.Object);

            var result = await handler.Handle(command, default);

            mockSet.Verify(m => m.Add(It.IsAny<Trade>()), Times.Once());
            _mockTradeRepo.Verify(m => m.SaveChangesAsync(default), Times.Once());
            _mockIPublishTrade.Verify(m=>m.CreatePublishTrade(It.IsAny<TradesMessage>()), Times.Once());
        }
        [Test]
        public async Task Handle_ShouldRequireMinimumFields()
        {
            var validator = new CreateTradeCommandValidator();
            var command = new CreateTradeCommand
            {
                CommoditiesIdentifier = "AU",
                Side = (Domain.Common.Side)2
            };
            
            var validationResult=await validator.ValidateAsync(command);


            Assert.That(validationResult.IsValid,Is.False);

        }
    }
}

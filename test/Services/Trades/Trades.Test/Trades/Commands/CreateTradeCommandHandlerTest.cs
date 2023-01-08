using EventBus.RabbitMQ.Trades.Notificaitons;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Reflection.Metadata;
using Trades.Application.Common.Interfaces;
using Trades.Application.PublishTrades.Interfaces;
using Trades.Application.Trades.Commands.CreateTrade;
using Trades.Domain.Entity;
using Trades.Infrastructure.Persistence;
using Trades.Infrastructure.Persistence.Interceptors;

namespace Trades.Test.Trades.Commands
{
    public class CreateTradeCommandHandlerTest
    {
        private readonly Mock<ITradeApplicationDbContext> _mockTradeRepo;
        private readonly Mock<IPublishTrade> _mockIPublishTrade;
        private readonly Mock<AuditableEntitySaveChangesInterceptor> _auditableEntitySaveChangesInterceptor;
        public CreateTradeCommandHandlerTest()
        {
            _mockTradeRepo = new();
            _mockIPublishTrade = new();
            _auditableEntitySaveChangesInterceptor = new();
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

            TradesMessage notificationMessage = new();

            var handler = new CreateTradeCommandHandler(_mockTradeRepo.Object, _mockIPublishTrade.Object);

            var result = await handler.Handle(command, default);

            mockSet.Verify(m => m.Add(It.IsAny<Trade>()), Times.Once());
            _mockTradeRepo.Verify(m => m.SaveChangesAsync(default), Times.Once());

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

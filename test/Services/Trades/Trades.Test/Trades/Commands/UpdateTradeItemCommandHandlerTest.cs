using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trades.Application.Common.Exceptions;
using Trades.Application.Common.Interfaces;
using Trades.Application.Trades.Commands.UpdateTrade;
using Trades.Domain.Entity;
using Trades.Test.Mocks;

namespace Trades.Test.Trades.Commands
{
    internal class UpdateTradeItemCommandHandlerTest
    {
        private Mock<ITradeApplicationDbContext> _context;
        private UpdateTradeCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            _context = new Mock<ITradeApplicationDbContext>();
            _handler = new UpdateTradeCommandHandler(_context.Object);
        }

        [Test]
        public async Task Handle_ShouldUpdateNotification()
        {
            IQueryable<Trade> data = MockTradeData.MockQueryableTradeData();

            // Arrange
            var command = new UpdateTradeStausCommand { Id = new Guid("26f0b3af-0d7a-4285-80ac-c4928375e8e1") };
            _context.Setup(c => c.Trades).Returns(data.AsQueryable().BuildMockDbSet().Object);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Test]
        public void Handle_ShouldThrowNotFoundException_WhenNotificationNotExist()
        {
            IQueryable<Trade> data = MockTradeData.MockQueryableTradeData();
            // Arrange
            var command = new UpdateTradeStausCommand { Id = new Guid("4936775b-b411-4b9a-9384-9be8278b7bd2") };
            _context.Setup(c => c.Trades).Returns(data.AsQueryable().BuildMockDbSet().Object);


            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}

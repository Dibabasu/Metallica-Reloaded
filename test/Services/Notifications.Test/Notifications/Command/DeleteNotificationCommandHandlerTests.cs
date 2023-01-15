using MockQueryable.Moq;
using Moq;
using Notifications.Application.Common.Exceptions;
using Notifications.Application.Common.Interfaces;
using Notifications.Application.Notifications.Commands.DeleteNotification;
using Notifications.Domain.Entity;
using Notifications.Test.Mock;

namespace Notifications.Test.Notifications.Command
{
    internal class DeleteNotificationCommandHandlerTests
    {
        private Mock<INotificationsDbContext> _context;
        private DeleteNotificaitonCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            _context = new Mock<INotificationsDbContext>();
            _handler = new DeleteNotificaitonCommandHandler(_context.Object);
        }

        [Test]
        public async Task Handle_ShouldDeleteNotification()
        {
            IQueryable<Notification> data = MockNotificaitonData.MockNotificationsData();

           
            // Arrange
            var id = Guid.NewGuid();
            var command = new DeleteNotificationCommand { Id = new Guid("4636775b-b411-4b9a-9384-9be8278b7bd2") };
            _context.Setup(c => c.Notifications).Returns(data.AsQueryable().BuildMockDbSet().Object);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Test]
        public void Handle_ShouldThrowNotFoundException_WhenNotificationNotExist()
        {
            IQueryable<TradeNotification> data = MockNotificaitonData.MockTradeNotificationsData();
            // Arrange
            var command = new DeleteNotificationCommand { Id = Guid.NewGuid() };
        _context.Setup(x => x.Notifications.FindAsync(command.Id, CancellationToken.None))
                .ReturnsAsync((Notification)null);

        // Act & Assert
        Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}

using MockQueryable.Moq;
using Moq;
using Notifications.Application.Common.Exceptions;
using Notifications.Application.Common.Interfaces;
using Notifications.Application.Notifications.Commands.UpdateNotification;
using Notifications.Domain.Entity;
using Notifications.Test.Mock;

namespace Notifications.Test.Notifications.Command
{
    internal class UpdateNotificationCommandHandlerTest
    {
        private Mock<INotificationsDbContext> _context;
        private UpdateNotificationCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            _context = new Mock<INotificationsDbContext>();
            _handler = new UpdateNotificationCommandHandler(_context.Object);
        }

        [Test]
        public async Task Handle_ShouldUpdateNotification()
        {
            IQueryable<Notification> data = MockNotificaitonData.MockNotificationsData();

            // Arrange
            var command = new UpdateNotificationCommand { Id = new Guid("4636775b-b411-4b9a-9384-9be8278b7bd2") };
            _context.Setup(c => c.Notifications).Returns(data.AsQueryable().BuildMockDbSet().Object);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Test]
        public void Handle_ShouldThrowNotFoundException_WhenNotificationNotExist()
        {
            IQueryable<Notification> data = MockNotificaitonData.MockNotificationsData();
            // Arrange
            var command = new UpdateNotificationCommand { Id = new Guid("4936775b-b411-4b9a-9384-9be8278b7bd2") };
            _context.Setup(c => c.Notifications).Returns(data.AsQueryable().BuildMockDbSet().Object);


            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}

using EventBus.RabbitMQ.Notifications.Communications;
using MediatR;
using MockQueryable.Moq;
using Moq;
using Notifications.Application.Common.Exceptions;
using Notifications.Application.Common.Interfaces;
using Notifications.Application.Notifications.Commands.CreateNotification;
using Notifications.Application.Notifications.Commands.UpdateNotification;
using Notifications.Application.PublishCommuncaitons.Interfaces;
using Notifications.Domain.Common;
using Notifications.Domain.Entity;
using Notifications.Test.Mock;

namespace Notifications.Test.Notifications.Command
{
    [TestFixture]
    internal class CreateNotificationHandlerTests
    {
        private Mock<INotificationsDbContext> _context;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _context = new Mock<INotificationsDbContext>();
            _mediator = new Mock<IMediator>();
        }
        [Test]
        public void Handle_WhenTradeIdNotFound_ShouldThrowNotFoundException()
        {
            IQueryable<TradeNotification> data = MockNotificaitonData.MockTradeNotificationsData();
            _context.Setup(c => c.TradeNotifications).Returns(data.AsQueryable().BuildMockDbSet().Object);

            var request = new CreateNotificaitonCommand
            {
                TradeId = new Guid("c815c4d8-14b3-480e-81da-846f60a95aed")
            };

            var service = new CreateNotificaitonHandler(_context.Object);

            Assert.That(() => service.Handle(request, default),
                Throws.InstanceOf<NotFoundException>());
        }
        [Test]
        public async Task Handle_WhenCalled_ShouldCreateNewNotification()
        {
            IQueryable<Notification> ndata = MockNotificaitonData.MockNotificationsData();
            IQueryable<TradeNotification> tdata = MockNotificaitonData.MockTradeNotificationsData();
            // Arrange
            var handler = new CreateNotificaitonHandler(_context.Object);


            _context.Setup(c => c.Notifications).Returns(ndata.AsQueryable().BuildMockDbSet().Object);
            _context.Setup(c => c.TradeNotifications).Returns(tdata.AsQueryable().BuildMockDbSet().Object);

            var request = new CreateNotificaitonCommand
            {
                TradeId = new Guid("63edbb65-063c-4f08-9669-d924d645f15a")
            };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            _context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once());
        }
        [Test]
        public void Handle_DuplicateTradeFound_ShouldThrowDuplicateTradeException()
        {
            IQueryable<Notification> ndata = MockNotificaitonData.MockNotificationsData();
            IQueryable<TradeNotification> tdata = MockNotificaitonData.MockTradeNotificationsData();
            // Arrange
            var handler = new CreateNotificaitonHandler(_context.Object);


            _context.Setup(c => c.Notifications).Returns(ndata.AsQueryable().BuildMockDbSet().Object);
            _context.Setup(c => c.TradeNotifications).Returns(tdata.AsQueryable().BuildMockDbSet().Object);

            var request = new CreateNotificaitonCommand
            {
                TradeId = new Guid("5efae644-1329-42e5-adf9-6b89796171ef")
            };


            var service = new CreateNotificaitonHandler(_context.Object);

            Assert.That(() => service.Handle(request, default),
                Throws.InstanceOf<DuplicateFoundException>());
        }
    }
}

using AutoMapper;
using MockQueryable.Moq;
using Moq;
using Notifications.Application.Common.Exceptions;
using Notifications.Application.Common.Interfaces;
using Notifications.Application.Notifications.Queries;
using Notifications.Application.Notifications.Queries.GetNotificationById;
using Notifications.Domain.Common;
using Notifications.Domain.Entity;
using Notifications.Test.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Test.Notifications.Queries
{
    [TestFixture]
    internal class GetNotificationByIdQueryTest
    {

        private Mock<INotificationsDbContext> _context;

        [SetUp]
        public void Setup()
        {
            _context = new Mock<INotificationsDbContext>();
        }

        [Test]
        public async Task ShouldReturnTradewhenFound_Notifications()
        {
            IQueryable<Notification> data = MockNotificaitonData.MockNotificationsData();

            _context.Setup(c => c.Notifications).Returns(data.AsQueryable().BuildMockDbSet().Object);

            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.ConfigurationProvider)
                .Returns(
                    () => new MapperConfiguration(
                        cfg => { cfg.CreateMap<Notification, NotificationDTO>(); }));

            var request = new GetNotificaitonbyIdQuery
            {
                Id = new Guid("4636775b-b411-4b9a-9384-9be8278b7bd2")
            };


            var service = new GetNotificaitonbyIdQueryHandler(_context.Object, mapper.Object);
            var notificaiton = await service.Handle(request, default);

            Assert.That(notificaiton.EmailStatus, Is.EqualTo(NotificaitonStatus.Sent));
            Assert.That(notificaiton.SMSStatus, Is.EqualTo(NotificaitonStatus.Sent));
            Assert.That(notificaiton, Is.Not.Null);
        }

        [Test]
        public void ShouldReturnNotFoundExceptionWhenNotFound_Notifications()
        {

            IQueryable<Notification> data = MockNotificaitonData.MockNotificationsData();

            _context.Setup(c => c.Notifications).Returns(data.AsQueryable().BuildMockDbSet().Object);

            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.ConfigurationProvider)
                .Returns(
                    () => new MapperConfiguration(
                        cfg => { cfg.CreateMap<Notification, NotificationDTO>(); }));

            var request = new GetNotificaitonbyIdQuery
            {
                Id = new Guid("5efae644-1329-42e5-adf9-6e89796171ef")
            };

            var service = new GetNotificaitonbyIdQueryHandler(_context.Object, mapper.Object);
            // Assert
            Assert.That(() => service.Handle(request, default),
                Throws.InstanceOf<NotFoundException>());
        }
        [Test]
        public async Task ShouldGiveValidationFailedIfTradeIdisNotPassed_Notifications()
        {
            var validator = new GetNotificaitonbyIdQueryValidator();
            var reqest = new GetNotificaitonbyIdQuery();

            var validationResult = await validator.ValidateAsync(reqest);

            // Assert
            Assert.That(validationResult.IsValid, Is.False);
        }
    }
}

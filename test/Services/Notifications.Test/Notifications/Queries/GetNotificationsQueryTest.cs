using AutoMapper;
using MockQueryable.Moq;
using Moq;
using Notifications.Application.Common.Interfaces;
using Notifications.Application.Notifications.Queries;
using Notifications.Application.Notifications.Queries.GetNotificationsWithPagination;
using Notifications.Domain.Entity;
using Notifications.Test.Mock;

namespace Notifications.Test.Notifications.Queries
{
    internal class GetNotificationsQueryTest
    {
        private Mock<INotificationsDbContext> _context;
        [SetUp]
        public void Setup()
        {
            _context = new Mock<INotificationsDbContext>();
        }

        [Test]
        public async Task ShouldReturnAll_Trades()
        {
            IQueryable<Notification> data = MockNotificaitonData.MockNotificationsData();

            _context.Setup(c => c.Notifications).Returns(data.AsQueryable().BuildMockDbSet().Object);

            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.ConfigurationProvider)
                .Returns(
                    () => new MapperConfiguration(
                        cfg => { cfg.CreateMap<Notification, NotificationDTO>(); }));

            var request = new GetNotificationsWithPaginationQuery();


            var service = new GetNotificationsWithPaginationQueryHandler(_context.Object, mapper.Object);
            var trades = await service.Handle(request, default);

            Assert.That(trades.TotalCount, Is.EqualTo(3));
        }
    }
}

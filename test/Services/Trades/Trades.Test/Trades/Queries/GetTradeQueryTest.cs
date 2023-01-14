using AutoMapper;
using MockQueryable.Moq;
using Moq;
using Trades.Application.Common.Interfaces;
using Trades.Application.Trades.Queries;
using Trades.Application.Trades.Queries.GetTradesWithPagination;
using Trades.Domain.Entity;
using Trades.Test.Mocks;

namespace Trades.Test.Trades.Queries
{
    [TestFixture]
    public class GetTradeQueryTest
    {
      
        private  Mock<ITradeApplicationDbContext> _mockTradeRepo;
        [SetUp]
        public void Setup()
        {
            _mockTradeRepo = new Mock<ITradeApplicationDbContext>();
        }

        [Test]
        public async Task ShouldReturnAll_Trades()
        {
            IQueryable<Trade> data = MockTradeData.MockQueryableTradeData();

            _mockTradeRepo.Setup(c => c.Trades).Returns(data.AsQueryable().BuildMockDbSet().Object);

            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.ConfigurationProvider)
                .Returns(
                    () => new MapperConfiguration(
                        cfg => { cfg.CreateMap<Trade, TradeDTO>(); }));

            var request = new GetTradesWithPaginationQuery();


            var service = new GetTradesWithPaginationQueryHandler(_mockTradeRepo.Object, mapper.Object);
            var trades = await service.Handle(request,default);

            Assert.That(trades.TotalCount, Is.EqualTo(3));
        }

    }
}

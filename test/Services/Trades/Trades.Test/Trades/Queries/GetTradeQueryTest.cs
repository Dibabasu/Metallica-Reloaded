using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Trades.Application.Common.Interfaces;
using Trades.Application.Common.Mappings;
using Trades.Application.Trades.Queries;
using Trades.Application.Trades.Queries.GetTradesWithPagination;
using Trades.Domain.Common;
using Trades.Domain.Entity;
using Trades.Test.Mocks;
using static TestDbAsyncQueryProvider<Trades.Domain.Entity.Trade>;

namespace Trades.Test.Trades.Queries
{
    public class GetTradeQueryTest
    {
      
        private readonly Mock<ITradeApplicationDbContext> _mockTradeRepo;

        public GetTradeQueryTest()
        {
            _mockTradeRepo = new();
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

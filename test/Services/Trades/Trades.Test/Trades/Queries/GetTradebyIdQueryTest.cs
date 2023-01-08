using AutoMapper;
using MockQueryable.Moq;
using Moq;
using Trades.Application.Common.Exceptions;
using Trades.Application.Common.Interfaces;
using Trades.Application.Trades.Commands.CreateTrade;
using Trades.Application.Trades.Queries;
using Trades.Application.Trades.Queries.GetTradeById;
using Trades.Domain.Entity;
using Trades.Test.Mocks;

namespace Trades.Test.Trades.Queries
{
    internal class GetTradebyIdQueryTest
    {
        private readonly Mock<ITradeApplicationDbContext> _mockTradeRepo;
        public GetTradebyIdQueryTest()
        {
            _mockTradeRepo = new Mock<ITradeApplicationDbContext>();
        }
        [Test]
        public async Task ShouldReturnTradewhenFound_Trades()
        {
            IQueryable<Trade> data = MockTradeData.MockQueryableTradeData();

            _mockTradeRepo.Setup(c => c.Trades).Returns(data.AsQueryable().BuildMockDbSet().Object);

            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.ConfigurationProvider)
                .Returns(
                    () => new MapperConfiguration(
                        cfg => { cfg.CreateMap<Trade, TradeDTO>(); }));

            var request = new GetTradebyIdQuery
            {
                Id = new Guid("5efae644-1329-42e5-adf9-6b89796171ef")
            };


            var service = new GetTradebyIdQueryHandler(_mockTradeRepo.Object, mapper.Object);
            var trades = await service.Handle(request, default);

            Assert.That(trades.Price, Is.EqualTo(121));
            Assert.That(trades, Is.Not.Null);
        }

        [Test]
        public async Task ShouldReturnNotFoundExceptionWhenNotFound_Trades()
        {
            IQueryable<Trade> data = MockTradeData.MockQueryableTradeData();

            _mockTradeRepo.Setup(c => c.Trades).Returns(data.AsQueryable().BuildMockDbSet().Object);

            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.ConfigurationProvider)
                .Returns(
                    () => new MapperConfiguration(
                        cfg => { cfg.CreateMap<Trade, TradeDTO>(); }));

            var request = new GetTradebyIdQuery
            {
                Id = new Guid("5efae644-1329-42e5-adf9-6e89796171ef")
            };


            var service = new GetTradebyIdQueryHandler(_mockTradeRepo.Object, mapper.Object);
            //  var trades = await service.Handle(request, default);

            Assert.That(() => service.Handle(request, default), 
                Throws.InstanceOf<NotFoundException>());
        }
        [Test]
        public async Task ShouldGiveValidationFailedIfTradeIdisNotPassed_Trades()
        {
            var validator = new GetTradebyIdQueryValidator();
            var reqest = new GetTradebyIdQuery();

            var validationResult = await validator.ValidateAsync(reqest);


            Assert.That(validationResult.IsValid, Is.False);
        }

    }
}

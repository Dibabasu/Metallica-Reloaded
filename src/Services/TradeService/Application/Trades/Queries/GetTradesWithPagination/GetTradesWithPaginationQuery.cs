using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Trades.Application.Common.Interfaces;
using Trades.Application.Common.Mappings;
using Trades.Application.Common.Models;

namespace Trades.Application.Trades.Queries.GetTradesWithPagination
{
    public class GetTradesWithPaginationQuery : IRequest<PaginatedList<TradeDTO>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
    public class GetTradesWithPaginationQueryHandler : IRequestHandler<GetTradesWithPaginationQuery, PaginatedList<TradeDTO>>
    {
        private readonly ITradeApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTradesWithPaginationQueryHandler(ITradeApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<TradeDTO>> Handle(GetTradesWithPaginationQuery request, CancellationToken cancellationToken)
        {

            return await
                _context.Trades
                .ProjectTo<TradeDTO>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}

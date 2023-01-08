using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Notifications.Application.Common.Interfaces;
using Notifications.Application.Common.Mappings;
using Notifications.Application.Common.Models;

namespace Notifications.Application.Notifications.Queries.GetNotificationsWithPagination
{
    public class GetTradesWithPaginationQuery : IRequest<PaginatedList<TradeDTO>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
    public class GetTradesWithPaginationQueryHandler : IRequestHandler<GetTradesWithPaginationQuery, PaginatedList<TradeDTO>>
    {
        private readonly INotificationsDbContext _context;
        private readonly IMapper _mapper;

        public GetTradesWithPaginationQueryHandler(INotificationsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<TradeDTO>> Handle(GetTradesWithPaginationQuery request, CancellationToken cancellationToken)
        {

            return await
                _context.Notifications
                .ProjectTo<TradeDTO>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}

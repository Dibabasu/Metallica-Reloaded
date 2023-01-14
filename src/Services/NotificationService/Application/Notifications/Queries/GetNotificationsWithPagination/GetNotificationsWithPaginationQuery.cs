using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Notifications.Application.Common.Interfaces;
using Notifications.Application.Common.Mappings;
using Notifications.Application.Common.Models;

namespace Notifications.Application.Notifications.Queries.GetNotificationsWithPagination
{
    public class GetNotificationsWithPaginationQuery : IRequest<PaginatedList<NotificationDTO>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
    public class GetNotificationsWithPaginationQueryHandler : IRequestHandler<GetNotificationsWithPaginationQuery, PaginatedList<NotificationDTO>>
    {
        private readonly INotificationsDbContext _context;
        private readonly IMapper _mapper;

        public GetNotificationsWithPaginationQueryHandler(INotificationsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<NotificationDTO>> Handle(GetNotificationsWithPaginationQuery request, CancellationToken cancellationToken)
        {

            return await
                _context.Notifications
                .ProjectTo<NotificationDTO>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}

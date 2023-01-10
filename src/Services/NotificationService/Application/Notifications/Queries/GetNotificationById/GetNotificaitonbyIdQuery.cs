using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notifications.Application.Common.Exceptions;
using Notifications.Application.Common.Interfaces;
using System.Threading;

namespace Notifications.Application.Notifications.Queries.GetNotificationById
{
    public class GetNotificaitonbyIdQuery : IRequest<NotificationDTO>
    {
        public Guid Id { get; set; }
    }
    public class GetNotificaitonbyIdQueryHandler : IRequestHandler<GetNotificaitonbyIdQuery, NotificationDTO>
    {
        private readonly INotificationsDbContext _context;
        private readonly IMapper _mapper;
        public GetNotificaitonbyIdQueryHandler(INotificationsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<NotificationDTO> Handle(GetNotificaitonbyIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Notifications
            .Where(l => l.Id == request.Id)
            .ProjectTo<NotificationDTO>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(GetNotificationById), request.Id);
            }

            return entity;
        }
    }
}

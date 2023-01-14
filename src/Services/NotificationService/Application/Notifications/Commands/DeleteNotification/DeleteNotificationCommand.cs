using MediatR;
using Microsoft.EntityFrameworkCore;
using Notifications.Application.Common.Exceptions;
using Notifications.Application.Common.Interfaces;

namespace Notifications.Application.Notifications.Commands.DeleteNotification
{
    public class DeleteNotificationCommand : IRequest
    {
        public Guid Id { get; init; }
    }
    public class DeleteNotificaitonCommandHandler : IRequestHandler<DeleteNotificationCommand>
    {
        private readonly INotificationsDbContext _context;

        public DeleteNotificaitonCommandHandler(INotificationsDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Notifications
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Notifications), request.Id);
            }

            _context.Notifications.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
    
}

using MediatR;
using Notifications.Application.Common.Exceptions;
using Notifications.Application.Common.Interfaces;
using Notifications.Domain.Common;

namespace Notifications.Application.Notifications.Commands.UpdateNotification
{
    public class UpdateNotificationCommand : IRequest
    {
        public Guid Id { get; init; }
        public NotificaitonStatus EmailStatus { get; init; }
        public NotificaitonStatus SMSStatus { get; init; }
        public int NumberOfRetries { get; set; } = 0;
    }

    public class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationCommand>
    {
        private readonly INotificationsDbContext _context;

        public UpdateNotificationCommandHandler(INotificationsDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Notifications
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Notifications), request.Id);
            }

            entity.EmailStatus = request.EmailStatus;
            entity.SMSStatus = request.SMSStatus;
            entity.EmailRetries = request.NumberOfRetries;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}

using EventBus.RabbitMQ.Notifications.Communications;
using MassTransit;
using MediatR;
using Notifications.Application.Common.Exceptions;
using Notifications.Application.Common.Interfaces;
using Notifications.Application.Notifications.Commands.UpdateNotification;
using Notifications.Application.PublishCommuncaitons.Interfaces;
using Notifications.Domain.Common;
using Notifications.Domain.Entity;
using Notifications.Domain.Events;

namespace Notifications.Application.Notifications.Commands.CreateNotification
{
    public class CreateNotificaitonCommand : IRequest<Guid>
    {
        public Guid TradeId { get; set; }

    }
    public class CreateNotificaitonHandler : IRequestHandler<CreateNotificaitonCommand, Guid>
    {
        private readonly INotificationsDbContext _context;

        public CreateNotificaitonHandler(INotificationsDbContext context)
        {
            _context = context;
           
        }
        public async Task<Guid> Handle(CreateNotificaitonCommand request, CancellationToken cancellationToken)
        {
            if (!CheckTradeExists(request.TradeId))
            {
                throw new NotFoundException("TradeId", request.TradeId);
            }

            if (DuplicateTradeCheck(request.TradeId))
            {
                throw new DuplicateFoundException("TradeId", request.TradeId);
            }

            var entity = new Notification
            {
                EmailRetries = 0,
                SMSStatus = NotificaitonStatus.Pending,
                EmailStatus = NotificaitonStatus.Pending,
                TradeId = request.TradeId
            };
            _context.Notifications.Add(entity);

            entity.AddDomainEvent(new NotificationCreatedEvent(entity));

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
        private bool CheckTradeExists(Guid tradeId)
        {
            return _context.TradeNotifications.Any(e => e.TradeId == tradeId);
        }
        private bool DuplicateTradeCheck(Guid tradeId)
        {
            return _context.Notifications.Any(e => e.TradeId == tradeId);
        }
    }
}

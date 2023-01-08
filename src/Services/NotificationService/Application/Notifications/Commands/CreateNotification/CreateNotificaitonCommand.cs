using EventBus.RabbitMQ.Notifications.Communications;
using MassTransit;
using MediatR;
using Notifications.Application.Common.Exceptions;
using Notifications.Application.Common.Interfaces;
using Notifications.Application.Notifications.Commands.UpdateNotification;
using Notifications.Application.PublishCommuncaitons.Interfaces;
using Notifications.Domain.Common;
using Notifications.Domain.Entity;

namespace Notifications.Application.Notifications.Commands.CreateNotification
{
    public class CreateNotificaitonCommand : IRequest<Guid>
    {
        public Guid TradeId { get; set; }

    }
    public class CreateNotificaitonHandler : IRequestHandler<CreateNotificaitonCommand, Guid>
    {
        private readonly INotificationsDbContext _context;
        private readonly IPublishNotification _publishNotification;
        private readonly IMediator _mediator;


        public CreateNotificaitonHandler(INotificationsDbContext context, IBus bus, IMediator mediator, IPublishNotification publishNotification)
        {
            _context = context;
            _mediator = mediator;
            _publishNotification = publishNotification;
        }
        public async Task<Guid> Handle(CreateNotificaitonCommand request, CancellationToken cancellationToken)
        {
            var data = _context.TradeNotifications
                .Where(d => d.TradeId == request.TradeId && d.IsActive == true)
                .Any();

            if (!data)
            {
                throw new NotFoundException("TradeId", request.TradeId);
            }

            var entity = new Notification
            {
                EmailRetries = 0,
                SMSStatus = NotificaitonStatus.Pending,
                EmailStatus = NotificaitonStatus.Pending,
                TradeId = request.TradeId
            };
            _context.Notifications.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            await _publishNotification.CreateNotificaiton(new NotificationMessage
            {
                NotificationId = entity.Id,
                TradeId = request.TradeId
            });

            await _mediator.Send(new UpdateNotificationCommand
            {
                EmailStatus = NotificaitonStatus.Enqueue,
                SMSStatus = NotificaitonStatus.Enqueue,
                Id = entity.Id

            }, cancellationToken);

            return entity.Id;
        }
    }
}

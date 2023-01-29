using EventBus.RabbitMQ.Notifications.Communications;
using MediatR;
using Microsoft.Extensions.Logging;
using Notifications.Application.Notifications.Commands.UpdateNotification;
using Notifications.Application.PublishCommuncaitons.Interfaces;
using Notifications.Domain.Common;
using Notifications.Domain.Entity;
using Notifications.Domain.Events;

namespace Notifications.Application.Notifications.EventHandlers;

public class NotificationCreatedEventHandler : INotificationHandler<NotificationCreatedEvent>
{
    private readonly IPublishNotification _publishNotification;
    private readonly ILogger<NotificationCreatedEventHandler> _logger;
    public NotificationCreatedEventHandler(IPublishNotification publishNotification, 
        ILogger<NotificationCreatedEventHandler> logger)
    {
        _publishNotification = publishNotification;
        _logger = logger;
    }

    public async Task Handle(NotificationCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Notificaiton Domain Event: {DomainEvent}", notification.GetType().Name);
        await PublishCreatedNotification(notification.Notification);

    }
    private async Task PublishCreatedNotification(Notification notification)
    {
        await _publishNotification.CreateNotificaiton(new NotificationMessage
        {
            NotificationId = notification.Id,
            TradeId = notification.TradeId
        });
    }
}



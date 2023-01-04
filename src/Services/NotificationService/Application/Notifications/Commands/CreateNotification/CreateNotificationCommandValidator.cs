using FluentValidation;

namespace Notifications.Application.Notifications.Commands.CreateNotification
{
    public class CreateNotificationCommandValidator : AbstractValidator<CreateNotificaitonCommand>
    {
        public CreateNotificationCommandValidator()
        {
            RuleFor(v => v.TradeId)
            .NotEmpty();
        }
    }
}

using FluentValidation;

namespace Notifications.Application.Notifications.Commands.UpdateNotification
{
    public class UpdateNotificationCommandValidator : AbstractValidator<UpdateNotificationCommand>
    {
        public UpdateNotificationCommandValidator()
        {
            RuleFor(v => v.Id)
            .NotEmpty();
        }
    }
}

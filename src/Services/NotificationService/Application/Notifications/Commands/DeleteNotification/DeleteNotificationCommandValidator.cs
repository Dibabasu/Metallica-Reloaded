using FluentValidation;
using Notifications.Application.Notifications.Commands.UpdateNotification;

namespace Notifications.Application.Notifications.Commands.DeleteNotification
{
    public class DeleteNotificationCommandValidator : AbstractValidator<DeleteNotificationCommand>
    {
        public DeleteNotificationCommandValidator()
        {
            RuleFor(v => v.Id)
               .NotEmpty();
        }
    }
}

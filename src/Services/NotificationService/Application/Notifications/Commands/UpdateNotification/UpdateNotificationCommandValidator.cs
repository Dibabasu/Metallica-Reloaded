using FluentValidation;

namespace Notifications.Application.Notifications.Commands.UpdateNotification
{
    public class UpdateNotificationCommandValidator : AbstractValidator<UpdateNotificationCommand>
    {
        public UpdateNotificationCommandValidator()
        {
            RuleFor(v => v.Id)
            .NotEmpty();
            RuleFor(v => v.SMSStatus)
                .NotNull();
            RuleFor(v => v.EmailStatus)
                .NotNull();
        }
    }
}

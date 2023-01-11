using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Notifications.Queries.GetNotificationById
{
    public class GetNotificaitonbyIdQueryValidator : AbstractValidator<GetNotificaitonbyIdQuery>
    {
        public GetNotificaitonbyIdQueryValidator()
        {
            RuleFor(x => x.Id)
           .NotEmpty().WithMessage("Please enter the Notificaiton");
        }
    }
}

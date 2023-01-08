using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trades.Application.Trades.Queries.GetTradeById
{
    public class GetTradebyIdQueryValidator : AbstractValidator<GetTradebyIdQuery>
    {
        public GetTradebyIdQueryValidator()
        {
            RuleFor(x => x.Id)
           .NotEmpty().WithMessage("Please enter the TradeId");
        }
    }
}

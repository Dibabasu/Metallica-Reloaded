using FluentValidation;

namespace Trades.Application.Trades.Commands.CreateTrade
{
    public class CreateTradeCommandValidator : AbstractValidator<CreateTradeCommand>
    {
        public CreateTradeCommandValidator()
        {
            RuleFor(v => v.CounterpartiesIdentifier)
            .NotEmpty();

            RuleFor(v => v.CommoditiesIdentifier)
            .NotEmpty();

            RuleFor(v => v.LocationIdentifier)
            .NotEmpty();

            RuleFor(v => v.Price)
                .GreaterThan(0);

            RuleFor(v => v.Quantity)
                .GreaterThan(0);

            RuleFor(v => v.Side)
                .NotEmpty();
        }
    }
}

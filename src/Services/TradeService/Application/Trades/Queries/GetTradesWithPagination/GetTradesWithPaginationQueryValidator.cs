using FluentValidation;

namespace Trades.Application.Trades.Queries.GetTradesWithPagination
{
    public class GetTradesWithPaginationQueryValidator : AbstractValidator<GetTradesWithPaginationQuery>
    {
        public GetTradesWithPaginationQueryValidator()
        {
            RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
        }
    }
}

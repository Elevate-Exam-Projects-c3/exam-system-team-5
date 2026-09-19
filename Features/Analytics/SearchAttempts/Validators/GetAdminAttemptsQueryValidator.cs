using exam_system.Features.Analytics.SearchAttempts.Queries;
using FluentValidation;

namespace exam_system.Features.Analytics.SearchAttempts.Validators
{
    public class GetAdminAttemptsQueryValidator : AbstractValidator<GetAdminAttemptsQuery>
    {
        public GetAdminAttemptsQueryValidator()
        {
            RuleFor(x => x.PageIndex)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageIndex must be at least 1.");

            RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100)
            .WithMessage("PageSize must be between 1 and 100.");

            RuleFor(x => x.Status)
            .IsInEnum()
            .When(x => x.Status.HasValue)
            .WithMessage("Invalid attempt status provided.");
        }
    }
}

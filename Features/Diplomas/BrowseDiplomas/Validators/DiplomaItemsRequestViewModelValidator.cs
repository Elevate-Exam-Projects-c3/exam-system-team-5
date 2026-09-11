using exam_system.Features.Diplomas.BrowseDiplomas.Controllers.ViewModels;
using exam_system.Features.Diplomas.BrowseDiplomas.Queries;
using FluentValidation;

namespace exam_system.Features.Diplomas.BrowseDiplomas.Validators
{
    public class DiplomaItemsRequestViewModelValidator : AbstractValidator<DiplomaItemsRequestViewModel>
    {
        public DiplomaItemsRequestViewModelValidator()
        {
            RuleFor(x => x.PageIndex)
              .GreaterThan(0)
              .WithMessage("Page index must be greater than 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage("Page size must be greater than 0.");
        }
    }
}

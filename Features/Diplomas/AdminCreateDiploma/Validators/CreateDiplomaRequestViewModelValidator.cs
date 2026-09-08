using exam_system.Features.Diplomas.AdminCreateDiploma.Commands.ViewModels;
using FluentValidation;

namespace exam_system.Features.Diplomas.AdminCreateDiploma.Validators
{
    public class CreateDiplomaRequestViewModelValidator : AbstractValidator<CreateDiplomaRequestViewModel>
    {
        public CreateDiplomaRequestViewModelValidator() 
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Diploma Title is required.")
                .Length(3, 200).WithMessage("Diploma Title must be between 3 and 200 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Diploma Description must not exceed 1000 characters.");

        }
    }
}

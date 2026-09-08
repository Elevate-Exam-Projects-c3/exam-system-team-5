using exam_system.Features.Diplomas.AdminUpdateDiploma.Controllers;
using FluentValidation;

namespace exam_system.Features.Diplomas.AdminUpdateDiploma.Validators
{
    public class UpdateDiplomaViewModelValidator : AbstractValidator<UpdateDiplomaViewModel>
    {
        public UpdateDiplomaViewModelValidator() 
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty).WithMessage("Diploma Id is required.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Diploma Title is required.")
                .Length(3, 200).WithMessage("Diploma Title must be between 3 and 200 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Diploma Description must not exceed 1000 characters.");

        }

    }
}

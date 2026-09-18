using exam_system.Features.Quizzes.AdminCreateQuiz.Controllers.ViewModels;
using FluentValidation;

namespace exam_system.Features.Quizzes.AdminCreateQuiz.Validators
{
    public class CreateQuizValidator:AbstractValidator<CreateQuizRequestViewModel>
    {
        public CreateQuizValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(3, 200).WithMessage("Title must be between 3 and 200 characters.");

            RuleFor(x => x.Instructions)
                .MaximumLength(4000).WithMessage("Instructions must not exceed 4000 characters.")
                .When(x => x.Instructions != null);

            RuleFor(x => x.DurationMinutes)
                .GreaterThan(0).WithMessage("DurationMinutes must be a positive integer.");

            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate).WithMessage("StartDate must be earlier than EndDate.");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate).WithMessage("EndDate must be later than StartDate.");

            RuleFor(x => x.PassScore)
                .InclusiveBetween(0, 100).WithMessage("PassScore must be between 0 and 100.");

            RuleFor(x => x.MaxAttempts)
                .GreaterThan(0).WithMessage("MaxAttempts must be a positive integer when provided.")
                .When(x => x.MaxAttempts.HasValue);
        }
    }
}

using FluentValidation;
using Sukun.Application.Dtos.Remembrance.Request;
using Sukun.Application.Dtos.RemembranceContent.Validation;

namespace Sukun.Application.Dtos.Remembrance.Validation
{
    public class RemembranceCreateDtoValidator : AbstractValidator<RemembranceCreateDto>
    {
        public RemembranceCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(300).WithMessage("Title cannot exceed 300 characters");

            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Text is required");

            RuleFor(x => x.RecommendedCount)
                .GreaterThanOrEqualTo(0).WithMessage("RecommendedCount must be zero or greater");

            RuleFor(x => x.CategoryIds)
                .NotNull().WithMessage("At least one category must be specified");

            RuleForEach(x => x.Contents)
                .SetValidator(new RemembranceContentCreateDtoValidator())
                .When(x => x.Contents != null && x.Contents.Any());
        }
    }
}

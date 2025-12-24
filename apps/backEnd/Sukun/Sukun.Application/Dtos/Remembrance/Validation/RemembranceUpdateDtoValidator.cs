using FluentValidation;
using Sukun.Application.Dtos.Remembrance.Request;
using Sukun.Application.Dtos.RemembranceContent.Validation;

namespace Sukun.Application.Dtos.Remembrance.Validation
{
    public class RemembranceUpdateDtoValidator : AbstractValidator<RemembranceUpdateDto>
    {
        public RemembranceUpdateDtoValidator()
        {
            RuleFor(x => x.Title)
                .MaximumLength(300).When(x => !string.IsNullOrEmpty(x.Title))
                .WithMessage("Title cannot exceed 300 characters");

            RuleFor(x => x.Text)
                .NotEmpty().When(x => !string.IsNullOrEmpty(x.Text))
                .WithMessage("Text cannot be empty if provided");

            RuleFor(x => x.RecommendedCount)
                .GreaterThanOrEqualTo(0).When(x => x.RecommendedCount > 0)
                .WithMessage("RecommendedCount must be zero or greater");

            RuleForEach(x => x.Contents)
                .SetValidator(new RemembranceContentCreateDtoValidator())
                .When(x => x.Contents != null && x.Contents.Any());
        }
    }
}

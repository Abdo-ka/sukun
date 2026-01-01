using FluentValidation;
using Sukun.Application.Dtos.NarrativeCategory.Request;

namespace Sukun.Application.Dtos.NarrativeCategory.Validation
{
    public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateDtoValidator()
        {
            RuleFor(x => x.Title)
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters")
                .When(x => !string.IsNullOrEmpty(x.Title));

            RuleFor(x => x.Order)
                .GreaterThanOrEqualTo(0).WithMessage("Order must be zero or positive")
                .When(x => x.Order.HasValue);
        }
    }
}

using FluentValidation;
using Sukun.Application.Dtos.NarrativeCategory.Request;

namespace Sukun.Application.Dtos.NarrativeCategory.Validation
{
    public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Category title is required")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

            RuleFor(x => x.TitleAr)
                .MaximumLength(200).WithMessage("Arabic title cannot exceed 200 characters")
                .When(x => x.TitleAr != null);

            RuleFor(x => x.Order)
                .GreaterThanOrEqualTo(0).WithMessage("Order must be zero or positive");
        }
    }
}

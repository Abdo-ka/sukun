using FluentValidation;
using Sukun.Application.Dtos.Tag.Request;

namespace Sukun.Application.Dtos.Tag.Validation
{
    public class TagUpdateDtoValidator : AbstractValidator<TagUpdateDto>
    {
        public TagUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tag name is required")
                .MaximumLength(100).WithMessage("Tag name cannot exceed 100 characters");

            RuleFor(x => x.NameAr)
                .MaximumLength(100).WithMessage("Arabic name cannot exceed 100 characters")
                .When(x => x.NameAr != null);
        }
    }
}

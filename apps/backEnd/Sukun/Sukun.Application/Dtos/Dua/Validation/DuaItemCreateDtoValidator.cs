using FluentValidation;
using Sukun.Application.Dtos.Dua.Request;

namespace Sukun.Application.Dtos.Dua.Validation
{
    public class DuaItemCreateDtoValidator : AbstractValidator<DuaItemCreateDto>
    {
        public DuaItemCreateDtoValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("CategoryId is required");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(300);

            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("ArabicText is required");
        }
    }
}

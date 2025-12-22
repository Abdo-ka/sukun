using FluentValidation;
using Sukun.Application.Dtos.IslamicBookSection.Request;

namespace Sukun.Application.Dtos.IslamicBook.Validation
{
    public class IslamicBookCreateDtoValidator : AbstractValidator<IslamicBookCreateDto>
    {
        public IslamicBookCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(300);

            RuleFor(x => x.NameAr)
                .NotEmpty().WithMessage("NameAr is required")
                .MaximumLength(300);

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Invalid BookType");
        }
    }
}


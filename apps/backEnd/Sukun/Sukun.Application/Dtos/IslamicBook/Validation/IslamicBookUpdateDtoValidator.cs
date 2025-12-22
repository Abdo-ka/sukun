using FluentValidation;
using Sukun.Application.Dtos.IslamicBookSection.Request;

namespace Sukun.Application.Dtos.IslamicBook.Validation
{
    public class IslamicBookUpdateDtoValidator : AbstractValidator<IslamicBookUpdateDto>
    {
        public IslamicBookUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(300).When(x => !string.IsNullOrEmpty(x.Name));

            RuleFor(x => x.NameAr)
                .MaximumLength(300).When(x => !string.IsNullOrEmpty(x.NameAr));

            RuleFor(x => x.Type)
                .IsInEnum().When(x => x.Type != 0);
        }
    }
}


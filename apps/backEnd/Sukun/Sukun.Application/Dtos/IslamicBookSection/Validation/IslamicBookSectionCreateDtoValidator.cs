using FluentValidation;
using Sukun.Application.Dtos.IslamicBook.Request;

namespace Sukun.Application.Dtos.IslamicBookSection.Validation
{
    public class IslamicBookSectionCreateDtoValidator : AbstractValidator<IslamicBookSectionCreateDto>
    {
        public IslamicBookSectionCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().MaximumLength(400);

            RuleFor(x => x.NameAr)
                .NotEmpty().MaximumLength(400);
        }
    }
}


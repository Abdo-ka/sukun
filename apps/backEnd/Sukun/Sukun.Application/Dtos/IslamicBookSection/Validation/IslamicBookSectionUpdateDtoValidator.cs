using FluentValidation;
using Sukun.Application.Dtos.IslamicBook.Request;
using Sukun.Application.Dtos.IslamicBook.Validation;

namespace Sukun.Application.Dtos.IslamicBookSection.Validation
{
    public class IslamicBookSectionUpdateDtoValidator : AbstractValidator<IslamicBookSectionUpdateDto>
    {
        public IslamicBookSectionUpdateDtoValidator()
        {
            Include(new IslamicBookSectionCreateDtoValidator());

            // اجعل الحقول optional
            RuleFor(x => x.Name).MaximumLength(400).When(x => !string.IsNullOrEmpty(x.Name));
            RuleFor(x => x.NameAr).MaximumLength(400).When(x => !string.IsNullOrEmpty(x.NameAr));
        }
    }
}


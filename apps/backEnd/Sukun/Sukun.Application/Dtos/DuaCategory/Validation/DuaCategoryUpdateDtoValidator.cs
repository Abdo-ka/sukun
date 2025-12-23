using FluentValidation;
using Sukun.Application.Dtos.DuaCategory.Request;

namespace Sukun.Application.Dtos.DuaCategory.Validation
{
    public class DuaCategoryUpdateDtoValidator : AbstractValidator<DuaCategoryUpdateDto>
    {
        public DuaCategoryUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.Name));

            RuleFor(x => x.NameAr)
                .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.NameAr));
        }
    }
}

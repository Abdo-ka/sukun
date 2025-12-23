using FluentValidation;
using Sukun.Application.Dtos.DuaCategory.Request;

namespace Sukun.Application.Dtos.DuaCategory.Validation
{
    public class DuaCategoryCreateDtoValidator : AbstractValidator<DuaCategoryCreateDto>
    {
        public DuaCategoryCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(200);

            RuleFor(x => x.NameAr)
                .NotEmpty().WithMessage("NameAr is required")
                .MaximumLength(200);
        }
    }
}

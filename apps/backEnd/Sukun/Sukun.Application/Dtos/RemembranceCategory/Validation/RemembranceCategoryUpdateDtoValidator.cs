using FluentValidation;
using Sukun.Application.Dtos.RemembranceCategory.Request;

namespace Sukun.Application.Dtos.RemembranceCategory.Validation
{
    public class RemembranceCategoryUpdateDtoValidator : AbstractValidator<RemembranceCategoryUpdateDto>
    {
        public RemembranceCategoryUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage("Name cannot exceed 200 characters");

        }
    }
}

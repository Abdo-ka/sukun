using FluentValidation;
using Sukun.Application.Dtos.NarrativeSection.Request;

namespace Sukun.Application.Dtos.NarrativeSection.Validation
{

    public class NarrativeSectionCreateDtoValidator : AbstractValidator<NarrativeSectionCreateDto>
    {
        public NarrativeSectionCreateDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Content).NotEmpty().MaximumLength(10000);
            RuleFor(x => x.DisplayOrder).GreaterThanOrEqualTo(0);
        }
    }
}

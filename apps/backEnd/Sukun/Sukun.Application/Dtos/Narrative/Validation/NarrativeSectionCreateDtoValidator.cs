using FluentValidation;
using Sukun.Application.Dtos.Narrative.Request;

namespace Sukun.Application.Dtos.Narrative.Validation
{

    public class NarrativeSectionCreateDtoValidator : AbstractValidator<NarrativeSectionCreateDto>
    {
        public NarrativeSectionCreateDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Content).NotEmpty().MaximumLength(10000); // حسب الحاجة
            RuleFor(x => x.DisplayOrder).GreaterThanOrEqualTo(0);
            RuleFor(x => x.MediaUrl).MaximumLength(500).When(x => x.MediaUrl != null);
        }
    }
}

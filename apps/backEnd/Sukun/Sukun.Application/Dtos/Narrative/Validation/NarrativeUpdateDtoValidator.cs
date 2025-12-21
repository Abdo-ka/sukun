using FluentValidation;
using Sukun.Application.Dtos.Narrative.Request;

namespace Sukun.Application.Dtos.Narrative.Validation
{

    public class NarrativeUpdateDtoValidator : AbstractValidator<NarrativeUpdateDto>
    {
        public NarrativeUpdateDtoValidator()
        {
            RuleFor(x => x.Title).MaximumLength(200).When(x => !string.IsNullOrEmpty(x.Title));
            RuleFor(x => x.Type).IsInEnum().When(x => x.Type.HasValue);
            RuleFor(x => x.ShortDescription).MaximumLength(500).When(x => x.ShortDescription != null);
            RuleFor(x => x.CoverImageUrl).MaximumLength(500).When(x => x.CoverImageUrl != null);
            RuleForEach(x => x.Sections).SetValidator(new NarrativeSectionUpdateDtoValidator()).When(x => x.Sections != null);
        }
    }
}

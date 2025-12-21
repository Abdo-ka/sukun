using FluentValidation;
using Sukun.Application.Dtos.Narrative.Request;

namespace Sukun.Application.Dtos.Narrative.Validation
{

    public class NarrativeCreateDtoValidator : AbstractValidator<NarrativeCreateDto>
    {
        public NarrativeCreateDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200).WithMessage("Title is required and must be <= 200 chars.");
            RuleFor(x => x.Type).IsInEnum().WithMessage("Invalid ContentType.");
            RuleFor(x => x.ShortDescription).MaximumLength(500);
            RuleFor(x => x.CoverImageUrl).MaximumLength(500).When(x => x.CoverImageUrl != null);
            RuleForEach(x => x.Sections).SetValidator(new NarrativeSectionCreateDtoValidator());
        }
    }
}

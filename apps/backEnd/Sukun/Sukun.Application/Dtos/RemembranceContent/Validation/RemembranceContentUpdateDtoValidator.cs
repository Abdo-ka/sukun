using FluentValidation;
using Sukun.Application.Dtos.RemembranceContent.Request;
using Sukun.Domin.Enums;

namespace Sukun.Application.Dtos.RemembranceContent.Validation
{
    public class RemembranceContentUpdateDtoValidator : AbstractValidator<RemembranceContentUpdateDto>
    {
        public RemembranceContentUpdateDtoValidator()
        {
            RuleFor(x => x.SourceType)
                .IsInEnum().WithMessage("Invalid SourceType");

            When(x => x.SourceType == SourceType.Custom, () =>
            {
                RuleFor(x => x.CustomContent)
                    .NotEmpty().WithMessage("CustomContent is required when SourceType is Custom");
             
                RuleFor(x => x.SourceId)
                   .Null().WithMessage("SourceId must be null when using Custom content.");

            });

            When(x => x.SourceType != SourceType.Custom, () =>
            {
                RuleFor(x => x.SourceId)
                    .NotEmpty().WithMessage("SourceId is required when SourceType is not Custom");

                RuleFor(x => x.CustomContent)
                .Null().WithMessage("CustomContent must be null when using an external source.");
            });
        }
    }
}

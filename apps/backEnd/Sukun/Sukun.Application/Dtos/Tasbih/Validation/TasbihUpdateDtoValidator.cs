using FluentValidation;
using Sukun.Application.Dtos.Tasbih.Request;

namespace Sukun.Application.Dtos.Tasbih.Validation
{
    public class TasbihUpdateDtoValidator : AbstractValidator<TasbihUpdateDto>
    {
        public TasbihUpdateDtoValidator()
        {
            RuleFor(x => x.Title)
                .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.Title));

            RuleFor(x => x.TitleAr)
                .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.TitleAr));

            RuleFor(x => x.RecommendedCount)
                .GreaterThanOrEqualTo(1).When(x => x.RecommendedCount > 0);
        }
    }
}

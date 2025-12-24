using FluentValidation;
using Sukun.Application.Dtos.Tasbih.Request;

namespace Sukun.Application.Dtos.Tasbih.Validation
{
    public class TasbihCreateDtoValidator : AbstractValidator<TasbihCreateDto>
    {
        public TasbihCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(200);

            RuleFor(x => x.TitleAr)
                .NotEmpty().WithMessage("TitleAr is required")
                .MaximumLength(200);

            RuleFor(x => x.RecommendedCount)
                .GreaterThanOrEqualTo(1).WithMessage("RecommendedCount must be at least 1");
        }
    }
}

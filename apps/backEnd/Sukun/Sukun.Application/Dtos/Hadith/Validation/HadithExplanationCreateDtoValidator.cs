using FluentValidation;
using Sukun.Application.Dtos.Hadith.Request;

namespace Sukun.Application.Dtos.Hadith.Validation
{
    public class HadithExplanationCreateDtoValidator : AbstractValidator<HadithExplanationCreateDto>
    {
        public HadithExplanationCreateDtoValidator()
        {
            RuleFor(x => x.Scholar).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Explanation).NotEmpty().MaximumLength(10000);
        }
    }
}

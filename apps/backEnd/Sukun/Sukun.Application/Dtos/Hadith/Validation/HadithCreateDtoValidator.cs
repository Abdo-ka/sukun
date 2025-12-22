using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Sukun.Application.Dtos.Hadith.Request;

namespace Sukun.Application.Dtos.Hadith.Validation
{
    public class HadithCreateDtoValidator : AbstractValidator<HadithCreateDto>
    {
        public HadithCreateDtoValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x => x.Reference).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Text).NotEmpty().MaximumLength(5000);
            RuleForEach(x => x.Explanations).SetValidator(new HadithExplanationCreateDtoValidator())
                .When(x => x.Explanations != null);
        }
    }
}

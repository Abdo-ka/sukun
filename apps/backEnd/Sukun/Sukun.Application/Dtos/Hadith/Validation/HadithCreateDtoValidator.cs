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
            RuleFor(x => x.Reference).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Text).NotEmpty().MaximumLength(5000);
            RuleForEach(x => x.Explanations).SetValidator(new HadithExplanationCreateDtoValidator())
                .When(x => x.Explanations != null);
            RuleFor(x => x.BookId)
                .NotNull().WithMessage("BookId is required when SectionId is specified")
                .When(x => x.SectionId.HasValue);
            RuleFor(x => x.SectionId)
                .Must(sectionId => !sectionId.HasValue)
                .WithMessage("SectionId cannot be specified when BookId is null")
                .When(x => x.BookId == null);
        }
    }
}

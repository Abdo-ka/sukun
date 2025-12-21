using Sukun.Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Sukun.Application.Dtos.Narrative.Request;

namespace Sukun.Application.Dtos.Narrative.Validation
{
    public class NarrativeSectionUpdateDtoValidator : AbstractValidator<NarrativeSectionUpdateDto>
    {
        public NarrativeSectionUpdateDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200).When(x => x.Title != null);
            RuleFor(x => x.Content).NotEmpty().MaximumLength(10000).When(x => x.Content != null);
            RuleFor(x => x.DisplayOrder).GreaterThanOrEqualTo(0);
            RuleFor(x => x.MediaUrl).MaximumLength(500).When(x => x.MediaUrl != null);
        }
    }
}

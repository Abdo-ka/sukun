using Sukun.Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Sukun.Application.Dtos.NarrativeSection.Request;
using Sukun.Infrastructure.InfrastructureBases;
using Microsoft.EntityFrameworkCore;

namespace Sukun.Application.Dtos.NarrativeSection.Validation
{
    public class NarrativeSectionUpdateDtoValidator : AbstractValidator<NarrativeSectionUpdateDto>
    {
        public NarrativeSectionUpdateDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200).When(x => x.Title != null);
            RuleFor(x => x.Content).NotEmpty().MaximumLength(10000).When(x => x.Content != null);
            RuleFor(x => x.DisplayOrder).GreaterThanOrEqualTo(0);
        }
    }
}

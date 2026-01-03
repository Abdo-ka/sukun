using FluentValidation;
using Sukun.Application.Dtos.RemembranceCategory.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukun.Application.Dtos.RemembranceCategory.Validation
{
    public class RemembranceCategoryCreateDtoValidator : AbstractValidator<RemembranceCategoryCreateDto>
    {
        public RemembranceCategoryCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");
    }
    }
}

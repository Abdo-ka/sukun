using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukun.Application.Dtos.AsmaulHusna.Validation
{
    public class GetAsmaulHusnaByNumberRequestValidator : AbstractValidator<GetAsmaulHusnaByNumberRequest>
    {
        public GetAsmaulHusnaByNumberRequestValidator()
        {
            RuleFor(x => x.Number)
                .InclusiveBetween(1, 99)
                .WithMessage("Number must be between 1 and 99");
        }
    }
}

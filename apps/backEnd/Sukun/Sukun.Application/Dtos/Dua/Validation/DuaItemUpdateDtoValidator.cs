using FluentValidation;
using Sukun.Application.Dtos.Dua.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukun.Application.Dtos.Dua.Validation
{

    public class DuaItemUpdateDtoValidator : AbstractValidator<DuaItemUpdateDto>
    {
        public DuaItemUpdateDtoValidator()
        {
            RuleFor(x => x.Title)
                .MaximumLength(300).When(x => !string.IsNullOrEmpty(x.Title));

            RuleFor(x => x.Text)
                .NotEmpty().When(x => !string.IsNullOrEmpty(x.Text));
        }
    }
}

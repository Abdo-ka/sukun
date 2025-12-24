using FluentValidation;
using Sukun.Application.Dtos.Mosque.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukun.Application.Dtos.Mosque.Validation
{

    public class MosqueUpdateDtoValidator : AbstractValidator<MosqueUpdateDto>
    {
        public MosqueUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.Name));

            RuleFor(x => x.NameAr)
                .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.NameAr));

            RuleFor(x => x.Address)
                .MaximumLength(500).When(x => !string.IsNullOrEmpty(x.Address));

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90).When(x => x.Latitude != 0);

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180).When(x => x.Longitude != 0);
        }
    }
}

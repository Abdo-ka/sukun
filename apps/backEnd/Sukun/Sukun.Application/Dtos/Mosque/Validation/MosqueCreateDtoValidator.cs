using FluentValidation;
using Sukun.Application.Dtos.Mosque.Request;

namespace Sukun.Application.Dtos.Mosque.Validation
{
    public class MosqueCreateDtoValidator : AbstractValidator<MosqueCreateDto>
    {
        public MosqueCreateDtoValidator()
        {
            RuleFor(x => x.CityId)
                .NotEmpty().WithMessage("CityId is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(200);

            RuleFor(x => x.NameAr)
                .NotEmpty().WithMessage("NameAr is required")
                .MaximumLength(200);

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required")
                .MaximumLength(500);

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90");

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180");
        }
    }
}

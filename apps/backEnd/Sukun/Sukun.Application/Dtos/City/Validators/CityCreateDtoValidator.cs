using FluentValidation;
using Sukun.Application.Dtos.City.Request;

namespace Sukun.Application.Dtos.City.Validators
{
    public class CityCreateDtoValidator : AbstractValidator<CityCreateDto>
    {
        public CityCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("City name is required")
                .MaximumLength(100);

            RuleFor(x => x.CountryCode)
                .NotEmpty().WithMessage("Country code is required")
                .Length(2).WithMessage("Country code must be 2 characters");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country name is required")
                .MaximumLength(100);

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90");

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180");

            RuleFor(x => x.TimeZone)
                .InclusiveBetween(-12, 14).WithMessage("Time zone must be between -12 and +14");
        }
    }

}

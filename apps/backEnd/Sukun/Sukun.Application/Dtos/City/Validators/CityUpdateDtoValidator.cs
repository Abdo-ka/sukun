using FluentValidation;
using Sukun.Application.Dtos.City.Request;

namespace Sukun.Application.Dtos.City.Validators
{
    public class CityUpdateDtoValidator : AbstractValidator<CityUpdateDto>
    {
        public CityUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(100).When(x => x.Name != null);

            RuleFor(x => x.NameAr)
                .MaximumLength(100).When(x => x.NameAr != null);

            RuleFor(x => x.Country)
                .MaximumLength(100).When(x => x.Country != null);

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90).When(x => x.Latitude.HasValue);

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180).When(x => x.Longitude.HasValue);

            RuleFor(x => x.TimeZone)
                .InclusiveBetween(-12, 14).When(x => x.TimeZone.HasValue);
        }
    }

}

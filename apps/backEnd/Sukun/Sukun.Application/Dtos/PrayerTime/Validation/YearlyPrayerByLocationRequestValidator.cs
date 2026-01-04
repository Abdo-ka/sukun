using FluentValidation;
using Sukun.Application.Dtos.PrayerTime.Request;

namespace Sukun.Application.Dtos.PrayerTime.Validation
{
    public class YearlyPrayerByLocationRequestValidator : AbstractValidator<YearlyPrayerByLocationRequest>
    {
        public YearlyPrayerByLocationRequestValidator()
        {
            RuleFor(x => x.Latitude)
                .NotNull().WithMessage("Latitude is required")
                .Must(lat => Math.Abs(lat) > 0.0001)
                    .WithMessage("Latitude cannot be 0")
                .InclusiveBetween(-90, 90)
                    .WithMessage("Latitude must be between -90 and 90");

            RuleFor(x => x.Longitude)
                .NotNull().WithMessage("Longitude is required")
                .Must(lng => Math.Abs(lng) > 0.0001)
                    .WithMessage("Longitude cannot be 0")
                .InclusiveBetween(-180, 180)
                    .WithMessage("Longitude must be between -180 and 180");

            RuleFor(x => x.Year)
                .NotEmpty().WithMessage("Year is required")
                .InclusiveBetween(1900, DateTime.UtcNow.Year + 10)
                    .WithMessage($"Year must be between 1900 and {DateTime.UtcNow.Year + 10}");
        }
    }
}

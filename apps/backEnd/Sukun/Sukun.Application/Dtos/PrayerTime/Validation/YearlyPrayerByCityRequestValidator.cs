using FluentValidation;
using Sukun.Application.Dtos.PrayerTime.Request;

namespace Sukun.Application.Dtos.PrayerTime.Validation
{
    public class YearlyPrayerByCityRequestValidator : AbstractValidator<YearlyPrayerByCityRequest>
    {
        public YearlyPrayerByCityRequestValidator()
        {
            RuleFor(x => x.Year)
                .NotEmpty().WithMessage("Year is required")
                .InclusiveBetween(1900, DateTime.UtcNow.Year + 10)
                    .WithMessage($"Year must be between 1900 and {DateTime.UtcNow.Year + 10}");
        }
    }
}

using FluentValidation;
using Sukun.Application.Dtos.FCMToken.Request;

namespace Sukun.Application.Dtos.FCMToken.Validators
{
    public class FCMTokenCreateDtoValidator : AbstractValidator<FCMTokenCreateDto>
    {
        public FCMTokenCreateDtoValidator()
        {
            RuleFor(x => x.UserDeviceId)
                .NotEmpty().WithMessage("User device ID is required");

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("FCM token is required")
                .MinimumLength(100).WithMessage("Invalid FCM token format");
        }
    }

}

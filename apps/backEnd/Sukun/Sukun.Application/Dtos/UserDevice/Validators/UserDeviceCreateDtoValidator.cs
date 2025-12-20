using FluentValidation;
using Sukun.Application.Dtos.UserDevice.Request;

namespace Sukun.Application.Dtos.UserDevice.Validators
{
    public class UserDeviceCreateDtoValidator : AbstractValidator<UserDeviceCreateDto>
    {
        public UserDeviceCreateDtoValidator()
        {
            RuleFor(x => x.DeviceId)
                .NotEmpty().WithMessage("Device ID is required")
                .MaximumLength(500).WithMessage("Device ID too long");

            RuleFor(x => x.DeviceType)
                .NotEmpty().WithMessage("Device type is required")
                .Must(type => new[] { "iOS", "Android", "Web" }.Contains(type))
                .WithMessage("Device type must be iOS, Android, or Web");

            RuleFor(x => x.DeviceModel)
                .NotEmpty().WithMessage("Device model is required")
                .MaximumLength(200).WithMessage("Device model too long");

            RuleFor(x => x.AppVersion)
                .MaximumLength(50).When(x => x.AppVersion != null);
        }
    }

}

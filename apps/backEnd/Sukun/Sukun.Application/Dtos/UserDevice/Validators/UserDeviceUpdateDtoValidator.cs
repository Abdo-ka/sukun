using FluentValidation;
using Sukun.Application.Dtos.UserDevice.Request;

namespace Sukun.Application.Dtos.UserDevice.Validators
{
    public class UserDeviceUpdateDtoValidator : AbstractValidator<UserDeviceUpdateDto>
    {
        public UserDeviceUpdateDtoValidator()
        {
            RuleFor(x => x.DeviceModel)
                .MaximumLength(200).When(x => x.DeviceModel != null);

            RuleFor(x => x.AppVersion)
                .MaximumLength(50).When(x => x.AppVersion != null);
        }
    }

}

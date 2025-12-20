using FluentValidation;
using Sukun.Application.Dtos.Admin.Request;

namespace Sukun.Application.Dtos.Admin.Validators
{
    public class AdminChangePasswordDtoValidator : AbstractValidator<AdminChangePasswordDto>
    {
        public AdminChangePasswordDtoValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Current password is required");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                .Matches(@"[A-Z]").WithMessage("Must contain uppercase")
                .Matches(@"[a-z]").WithMessage("Must contain lowercase")
                .Matches(@"[0-9]").WithMessage("Must contain digit")
                .Matches(@"[!@#$%^&*]").WithMessage("Must contain special character")
                .NotEqual(x => x.CurrentPassword).WithMessage("New password cannot be the same as current");
        }
    }

}

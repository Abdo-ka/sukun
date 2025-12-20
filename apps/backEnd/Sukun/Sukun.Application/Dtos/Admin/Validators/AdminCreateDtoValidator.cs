using FluentValidation;
using Sukun.Application.Dtos.Admin.Request;
using Sukun.Infrastructure.Abstracts;

namespace Sukun.Application.Dtos.Admin.Validators
{
    public class AdminCreateDtoValidator : AbstractValidator<AdminCreateDto>
    {
        public AdminCreateDtoValidator(IAdminRepository adminRepository)
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MustAsync(async (email, ct) => await adminRepository.IsEmailUniqueAsync(email))
                .WithMessage("Email already exists");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                .Matches(@"[A-Z]").WithMessage("Password must contain uppercase letter")
                .Matches(@"[a-z]").WithMessage("Password must contain lowercase letter")
                .Matches(@"[0-9]").WithMessage("Password must contain digit")
                .Matches(@"[!@#$%^&*]").WithMessage("Password must contain special character");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required")
                .MaximumLength(200);
        }
    }

}

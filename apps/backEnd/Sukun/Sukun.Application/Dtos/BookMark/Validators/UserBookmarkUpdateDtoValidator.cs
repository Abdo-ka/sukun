using FluentValidation;
using Sukun.Application.Dtos.BookMark.Request;

namespace Sukun.Application.Dtos.BookMark.Validators
{
    public class UserBookmarkUpdateDtoValidator : AbstractValidator<UserBookmarkUpdateDto>
    {
        public UserBookmarkUpdateDtoValidator()
        {
            RuleFor(x => x.Note)
                .MaximumLength(500).When(x => x.Note != null)
                .WithMessage("Note cannot exceed 500 characters");

            RuleFor(x => x.Type)
                .IsInEnum().When(x => x.Type.HasValue)
                .WithMessage("Invalid bookmark type");
        }
    }

}

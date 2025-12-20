using FluentValidation;
using Sukun.Application.Dtos.BookMark.Request;
using Sukun.Infrastructure.Abstracts;

namespace Sukun.Application.Dtos.BookMark.Validators
{
    public class UserBookmarkCreateDtoValidator : AbstractValidator<UserBookmarkCreateDto>
    {
        public UserBookmarkCreateDtoValidator(IQuranRepository quranRepository)
        {
            RuleFor(x => x.VerseId)
                .NotEmpty().WithMessage("Verse ID is required")
                .MustAsync(async (verseId, ct) => await quranRepository.GetVerseByIdAsync(verseId) != null)
                .WithMessage("Selected verse does not exist");

            RuleFor(x => x.Note)
                .MaximumLength(500).WithMessage("Note cannot exceed 500 characters");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Invalid bookmark type");
        }
    }

}

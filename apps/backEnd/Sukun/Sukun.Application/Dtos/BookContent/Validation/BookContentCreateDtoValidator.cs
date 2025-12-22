using FluentValidation;
using Sukun.Application.Dtos.BookContent.Request;

namespace Sukun.Application.Dtos.BookContent.Validation
{
    public class BookContentCreateDtoValidator : AbstractValidator<BookContentCreateDto>
    {
        public BookContentCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(300);

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required");

            RuleFor(x => x.DisplayOrder)
                .GreaterThanOrEqualTo(1);
        }
    }
}


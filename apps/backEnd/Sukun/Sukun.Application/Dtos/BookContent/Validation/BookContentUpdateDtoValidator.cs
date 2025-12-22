using FluentValidation;
using Sukun.Application.Dtos.BookContent.Request;

namespace Sukun.Application.Dtos.BookContent.Validation
{

    public class BookContentUpdateDtoValidator : AbstractValidator<BookContentUpdateDto>
    {
        public BookContentUpdateDtoValidator()
        {
            RuleFor(x => x.Title)
                .MaximumLength(300).When(x => !string.IsNullOrEmpty(x.Title));

            RuleFor(x => x.Content)
                .NotEmpty().When(x => !string.IsNullOrEmpty(x.Content));

            RuleFor(x => x.DisplayOrder)
                .GreaterThanOrEqualTo(1);
        }
    }
}


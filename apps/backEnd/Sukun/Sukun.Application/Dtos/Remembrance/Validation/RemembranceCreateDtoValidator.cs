using FluentValidation;
using Sukun.Application.Dtos.Remembrance.Request;
using Sukun.Application.Dtos.RemembranceContent.Validation;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Application.Dtos.Remembrance.Validation
{
    public class RemembranceCreateDtoValidator : AbstractValidator<RemembranceCreateDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemembranceCreateDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(300).WithMessage("Title cannot exceed 300 characters");

            RuleFor(x => x.RecommendedCount)
                .GreaterThanOrEqualTo(0).WithMessage("RecommendedCount must be zero or greater");

            RuleFor(x => x.CategoryIds)
             .NotNull().WithMessage("CategoryIDs are required")
             .NotEmpty().WithMessage("At least one category must be specified")
             .Must(BeUnique).WithMessage("Duplicate CategoryIds are not allowed")
             .MustAsync(BeExistingCategories).WithMessage("One or more CategoryIds do not exist");


            RuleFor(x => x.Contents)
            .NotNull().WithMessage("Contents are required")
            .NotEmpty().WithMessage("At least one content item is required for the remembrance.")
            .ForEach(rc => rc.SetValidator(new RemembranceContentCreateDtoValidator()));
        }
        private bool BeUnique(List<Guid> ids)
        {
            return ids.Distinct().Count() == ids.Count;
        }

        private async Task<bool> BeExistingCategories(List<Guid> categoryIds, CancellationToken token)
        {
            if (!categoryIds.Any()) return true;
            var count = await _unitOfWork.RemembranceCategories.CountAsync(c => categoryIds.Contains(c.Id));
            return count == categoryIds.Count;
        }

    }
}

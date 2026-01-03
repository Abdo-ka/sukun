using FluentValidation;
using Sukun.Application.Dtos.NarrativeSection.Request;
using Sukun.Application.Dtos.Remembrance.Request;
using Sukun.Application.Dtos.RemembranceContent.Request;
using Sukun.Application.Dtos.RemembranceContent.Validation;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Application.Dtos.Remembrance.Validation
{
    public class RemembranceUpdateDtoValidator : AbstractValidator<RemembranceUpdateDto>
    {
        public IUnitOfWork _unitOfWork {  get; set; }
        public RemembranceUpdateDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.Title)
                .MaximumLength(300).When(x => !string.IsNullOrEmpty(x.Title))
                .WithMessage("Title cannot exceed 300 characters");

            RuleFor(x => x.RecommendedCount)
                .GreaterThanOrEqualTo(0).When(x => x.RecommendedCount > 0)
                .WithMessage("RecommendedCount must be zero or greater");

            RuleFor(x => x.Contents)
                .NotNull().WithMessage("Contents list cannot be empty.")
                .NotEmpty().WithMessage("Contents list cannot be empty.")
                .Must(HaveUniqueIds).WithMessage("Duplicate Content Ids are not allowed")
                .ForEach(x => x.SetValidator(new RemembranceContentUpdateDtoValidator()));

            RuleFor(x => x.CategoryIds)
              .NotEmpty().WithMessage("CategoryIds are required")
              .NotEmpty().WithMessage("At least one CategoryId must be provided")
              .Must(BeUnique).WithMessage("Duplicate CategoryIds are not allowed")
              .MustAsync(BeExistingCategories).WithMessage("One or more CategoryIds do not exist")
              ;
        }
        private bool BeUnique(List<Guid> ids) => ids.Distinct().Count() == ids.Count;
        private bool HaveUniqueIds(List<RemembranceContentUpdateDto> remembranceContents)
        {
            var ids = remembranceContents.Where(s => s.Id.HasValue).Select(s => s.Id.Value).ToList();
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

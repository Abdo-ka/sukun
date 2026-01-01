using FluentValidation;
using Sukun.Application.Dtos.Narrative.Request;
using Sukun.Application.Dtos.NarrativeSection.Request;
using Sukun.Application.Dtos.NarrativeSection.Validation;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Application.Dtos.Narrative.Validation
{
    public class NarrativeUpdateDtoValidator : AbstractValidator<NarrativeUpdateDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public NarrativeUpdateDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.Title)
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters")
                .When(x => !string.IsNullOrEmpty(x.Title));

            RuleFor(x => x.TitleAr)
                .MaximumLength(200).WithMessage("Arabic title cannot exceed 200 characters")
                .When(x => x.TitleAr != null);

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Invalid content type");


            RuleFor(x => x.Sections)
                .NotNull().WithMessage("Sections are required")
                .NotEmpty().WithMessage("At least one section must be provided")
                .Must(HaveUniqueIds).WithMessage("Duplicate section Ids are not allowed")
                .ForEach(section => section.SetValidator(new NarrativeSectionUpdateDtoValidator()));

            RuleFor(x => x.CategoryIds)
                .NotEmpty().WithMessage("CategoryIds are required")
                .NotEmpty().WithMessage("At least one CategoryId must be provided")
                .Must(BeUnique).WithMessage("Duplicate CategoryIds are not allowed")
                .MustAsync(BeExistingCategories).WithMessage("One or more CategoryIds do not exist")
                ;

            RuleFor(x => x.TagIds)
                .Must(BeUnique).WithMessage("Duplicate TagIds are not allowed")
                .MustAsync(BeExistingTags).WithMessage("One or more TagIds do not exist")
                .When(x => x.TagIds != null && x.TagIds.Any());
        }

        private bool BeUnique(List<Guid> ids) => ids.Distinct().Count() == ids.Count;
        private bool HaveUniqueIds(List<NarrativeSectionUpdateDto> sections)
        {
            var ids = sections.Where(s => s.Id.HasValue).Select(s => s.Id.Value).ToList();
            return ids.Distinct().Count() == ids.Count;
        }
        private async Task<bool> BeExistingCategories(List<Guid> ids, CancellationToken token)
        {
            var count = await _unitOfWork.Categories.CountAsync(c => ids.Contains(c.Id));
            return count == ids.Count;
        }

        private async Task<bool> BeExistingTags(List<Guid> ids, CancellationToken token)
        {
            var count = await _unitOfWork.Tags.CountAsync(t => ids.Contains(t.Id));
            return count == ids.Count;
        }
    }
}

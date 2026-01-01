using FluentValidation;
using Sukun.Application.Dtos.Narrative.Request;
using Sukun.Application.Dtos.NarrativeSection.Validation;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Application.Dtos.Narrative.Validation
{

    public class NarrativeCreateDtoValidator : AbstractValidator<NarrativeCreateDto>
    {
        private readonly IUnitOfWork _unitOfWork;

            public NarrativeCreateDtoValidator(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;

                RuleFor(x => x.Title)
                    .NotEmpty().WithMessage("Title is required")
                    .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

                RuleFor(x => x.TitleAr)
                    .MaximumLength(200).WithMessage("Arabic title cannot exceed 200 characters")
                    .When(x => x.TitleAr != null);

                RuleFor(x => x.Type)
                    .IsInEnum().WithMessage("Invalid content type");

                RuleFor(x => x.ShortDescription)
                    .MaximumLength(500).WithMessage("Short description cannot exceed 500 characters")
                    .When(x => x.ShortDescription != null);

                RuleFor(x => x.Sections)
                    .NotNull().WithMessage("Sections are required")
                    .NotEmpty().WithMessage("At least one section must be specified")
                    .ForEach(section => section.SetValidator(new NarrativeSectionCreateDtoValidator()));

                RuleFor(x => x.CategoryIds)
                    .NotNull().WithMessage("CategoryIDs are required")
                    .NotEmpty().WithMessage("At least one category must be specified")
                    .Must(BeUnique).WithMessage("Duplicate CategoryIds are not allowed")
                    .MustAsync(BeExistingCategories).WithMessage("One or more CategoryIds do not exist");

                RuleFor(x => x.TagIds)
                    .NotNull().WithMessage("TagIds cannot be null")
                    .Must(BeUnique).WithMessage("Duplicate TagIds are not allowed")
                    .MustAsync(BeExistingTags).WithMessage("One or more TagIds do not exist");
            }



        private bool BeUnique(List<Guid> ids)
        {
            return ids.Distinct().Count() == ids.Count;
        }

        private async Task<bool> BeExistingCategories(List<Guid> categoryIds, CancellationToken token)
        {
            if (!categoryIds.Any()) return true;
            var count = await _unitOfWork.Categories.CountAsync(c => categoryIds.Contains(c.Id));
            return count == categoryIds.Count;
        }

        private async Task<bool> BeExistingTags(List<Guid> tagIds, CancellationToken token)
        {
            if (!tagIds.Any()) return true;
            var count = await _unitOfWork.Tags.CountAsync(t => tagIds.Contains(t.Id));
            return count == tagIds.Count;
        }
    }
}

using Sukun.Application.Dtos.BookContent.Response;
using Sukun.Application.Dtos.IslamicBook.Response;
using Sukun.Application.Dtos.IslamicBookSection.Request;
using Sukun.Application.Dtos.IslamicBookSection.Response;
using Sukun.Domin.Entities;

namespace Sukun.Application.Mapper
{
    public static class IslamicBookMapper
    {
        public static IslamicBookListDto ToListDto(this IslamicBook book)
        {
            return new IslamicBookListDto
            {
                Id = book.Id,
                Name = book.Name,
                NameAr = book.NameAr,
                NameEn = book.NameEn,
                Author = book.Author,
                Type = book.Type,
                IconUrl = book.IconUrl,
                Order = book.Order,
                HadithsCount = book.Hadiths.Count(h => !h.IsDeleted)
            };
        }

        public static IslamicBookResponseDto ToResponseDto(this IslamicBook book)
        {
            return new IslamicBookResponseDto
            {
                Id = book.Id,
                Name = book.Name,
                NameAr = book.NameAr,
                NameEn = book.NameEn,
                Author = book.Author,
                Description = book.Description,
                Type = book.Type,
                Order = book.Order,
                IconUrl = book.IconUrl,
                HadithsCount = book.Hadiths.Count(h => !h.IsDeleted),
                SectionsCount = book.Sections.Count(s => !s.IsDeleted),
                Sections = book.Sections
                    .Where(s => !s.IsDeleted)
                    .OrderBy(s => s.Order)
                    .Select(s => s.ToSectionDto())
                    .ToList()
            };
        }

        public static IslamicBookSectionResponseDto ToSectionDto(this IslamicBookSection section)
        {
            return new IslamicBookSectionResponseDto
            {
                Id = section.Id,
                Name = section.Name,
                NameAr = section.NameAr,
                NameEn = section.NameEn,
                Description = section.Description,
                Order = section.Order,
                HadithsCount = section.Hadiths.Count(h => !h.IsDeleted),
                Contents = section.Contents.Where(c => !c.IsDeleted)
                                            .OrderBy(c => c.DisplayOrder)
                                            .Select(c => c.ToContentDto())
                                            .ToList()
            };
        }

        // للـ Create
        public static IslamicBook ToEntity(this IslamicBookCreateDto dto)
        {
            return new IslamicBook
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                NameAr = dto.NameAr,
                NameEn = dto.NameEn,
                Author = dto.Author,
                Description = dto.Description,
                Type = dto.Type,
                Order = dto.Order,
                IconUrl = dto.IconUrl,
                CreateAt = DateTime.UtcNow
            };
        }
        public static BookContentResponseDto ToContentDto(this BookContent content)
        {
            return new BookContentResponseDto
            {
                Id = content.Id,
                Title = content.Title,
                Content = content.Content,
                DisplayOrder = content.DisplayOrder,
                MediaUrl = content.MediaUrl
            };
        }
    }
}



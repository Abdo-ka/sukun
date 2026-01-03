using Sukun.Application.Dtos.Dua.Request;
using Sukun.Application.Dtos.Dua.Response;
using Sukun.Application.Dtos.DuaCategory.Request;
using Sukun.Application.Dtos.DuaCategory.Response;
using Sukun.Domin.Entities;

namespace Sukun.Application.Mapper
{
    public static class DuaMapper
    {
        public static DuaCategoryResponseDto ToResponseDto(this DuaCategory category)
        {
            return new DuaCategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                NameAr = category.NameAr,
                NameEn = category.NameEn,
                DuasCount = category.Duas.Count(d => !d.IsDeleted)
            };
        }

        public static DuaCategoryWithDuasDto ToCategoryWithDuasDto(this DuaCategory category)
        {
            return new DuaCategoryWithDuasDto
            {
                Id = category.Id,
                Name = category.Name,
                NameAr = category.NameAr,
                NameEn = category.NameEn,
                DuasCount = category.Duas.Count(d => !d.IsDeleted),
                Duas = category.Duas
                    .Where(d => !d.IsDeleted)
                    .OrderBy(d => d.DisplayOrder)
                    .Select(d => d.ToResponseDto())
                    .ToList()
            };
        }

        public static DuaItemResponseDto ToResponseDto(this DuaItem dua)
        {
            return new DuaItemResponseDto
            {
                Id = dua.Id,
                Title = dua.Title,
                Text = dua.Text,
                TextEn = dua.TextEn ?? "",
                Reference = dua.Reference,
                Virtue = dua.Virtue,
                DisplayOrder = dua.DisplayOrder,
                CategoryId = dua.CategoryId,
                CategoryName = dua.Category?.NameAr ?? string.Empty
            };
        }

        public static DuaItemListResponseDto ToListDto(this DuaItem dua)
        {
            return new DuaItemListResponseDto
            {
                Id = dua.Id,
                Title = dua.Title,
                Text= dua.Text,
                TextEn= dua.TextEn ?? "",
                DisplayOrder = dua.DisplayOrder
            };
        }

        public static DuaCategory ToEntity(this DuaCategoryCreateDto dto)
        {
            return new DuaCategory
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                NameAr = dto.NameAr,
                NameEn = dto.NameEn,
                CreateAt = DateTime.UtcNow
            };
        }

        public static DuaItem ToEntity(this DuaItemCreateDto dto)
        {
            return new DuaItem
            {
                Id = Guid.NewGuid(),
                CategoryId = dto.CategoryId,
                Title = dto.Title,
                Text = dto.Text,
                TextEn = dto.TextEn,
                Reference = dto.Reference,
                Virtue = dto.Virtue,
                DisplayOrder = dto.DisplayOrder,
                CreateAt = DateTime.UtcNow
            };
        }
    }
}



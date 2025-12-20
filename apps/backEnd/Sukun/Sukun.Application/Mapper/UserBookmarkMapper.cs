using Sukun.Application.Dtos.BookMark.Request;
using Sukun.Application.Dtos.BookMark.Responce;
using Sukun.Application.Dtos.QuranVerse.Response;
using Sukun.Application.Dtos.User_Entity.Request;
using Sukun.Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukun.Application.Mapper
{
    public static class UserBookmarkMapper
    {
        public static UserBookmarkResponseDto ToResponseDto(UserBookmark bookmark)
        {
            return new UserBookmarkResponseDto
            {
                Id = bookmark.Id,
                UserId = bookmark.UserId,
                VerseId = bookmark.VerseId,
                Note = bookmark.Note,
                Type = bookmark.Type,
                Verse = bookmark.Verse != null ? QuranMapper.ToResponseDto(bookmark.Verse) : new QuranVerseResponseDto(),
                CreateAt = bookmark.CreateAt,
                UpdateAt = bookmark.UpdatedAt
            };
        }
        public static UserBookmark FromCreateDto(UserBookmarkCreateDto dto, Guid userId)
        {
            return new UserBookmark
            {
                UserId = userId,
                VerseId = dto.VerseId,
                Note = dto.Note,
                Type = dto.Type,
                CreateAt = DateTime.UtcNow
            };
        }

        public static void UpdateFromDto(UserBookmark bookmark, UserBookmarkUpdateDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.Note))
                bookmark.Note = dto.Note;
            if (dto.Type.HasValue)
                bookmark.Type = dto.Type.Value;

            bookmark.UpdatedAt = DateTime.UtcNow;
        }
    }
}

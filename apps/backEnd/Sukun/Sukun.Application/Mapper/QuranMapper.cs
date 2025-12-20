using Sukun.Application.Dtos.QuranSurah.Response;
using Sukun.Application.Dtos.QuranVerse.Response;
using Sukun.Application.Dtos.Tafsir.Response;
using Sukun.Domin.Entities;

namespace Sukun.Application.Mapper
{
    public static class QuranMapper
    {
        public static QuranSurahResponseDto ToResponseDto(QuranSurah surah)
        {
            return new QuranSurahResponseDto
            {
                Id = surah.Id,
                Number = surah.Number,
                Name = surah.Name,
                EnglishName = surah.EnglishName,
                RevelationOrder = surah.RevelationOrder,
                RevelationType = surah.RevelationType,
                TotalVerses = surah.TotalVerses,
                CreateAt = surah.CreateAt,
                UpdateAt = surah.UpdatedAt
            };
        }

        public static QuranSurahWithVersesResponseDto ToDetailDto(QuranSurah surah)
        {
            return new QuranSurahWithVersesResponseDto
            {
                Id = surah.Id,
                Number = surah.Number,
                Name = surah.Name,
                EnglishName = surah.EnglishName,
                RevelationOrder = surah.RevelationOrder,
                RevelationType = surah.RevelationType,
                TotalVerses = surah.TotalVerses,
                Verses = surah.Verses?.Select(x => ToResponseDto(x)) ?? new List<QuranVerseResponseDto>(),
            };
        }

        public static QuranVerseResponseDto ToResponseDto(QuranVerse verse)
        {
            return new QuranVerseResponseDto
            {
                Id = verse.Id,
                SurahId = verse.SurahId,
                VerseNumber = verse.VerseNumber,
                PageNumber = verse.PageNumber,
                JuzNumber = verse.JuzNumber,
                HizbNumber = verse.HizbNumber,
                Text = verse.Text,
                SurahName = verse.Surah?.Name ?? string.Empty,
                CreateAt = verse.CreateAt,
                UpdateAt = verse.UpdatedAt
            };
        }

        public static QuranVerseWithDetailsResponseDto ToDetailDto(QuranVerse verse)
        {
            return new QuranVerseWithDetailsResponseDto
            {
                Id = verse.Id,
                SurahId = verse.SurahId,
                VerseNumber = verse.VerseNumber,
                PageNumber = verse.PageNumber,
                JuzNumber = verse.JuzNumber,
                HizbNumber = verse.HizbNumber,
                Text = verse.Text,
                Surah = verse.Surah != null ? ToResponseDto(verse.Surah) : new QuranSurahResponseDto(),
                Tafsirs = verse.Tafsirs?.Select(ToTafsirResponseDto) ?? new List<TafsirResponseDto>(),
            };
        }

        public static TafsirResponseDto ToTafsirResponseDto(Tafsir tafsir)
        {
            return new TafsirResponseDto
            {
                Id = tafsir.Id,
                VerseId = tafsir.VerseId,
                Source = tafsir.Source,
                Author = tafsir.Author,
                Text = tafsir.Text,
                CreateAt = tafsir.CreateAt,
                UpdateAt = tafsir.UpdatedAt
            };
        }
    }
}

using Sukun.Domin.Enums;
using Sukun.Infrastructure.Abstracts;
using Microsoft.EntityFrameworkCore;
using Sukun.Infrastructure.InfrastructureBases;
using Sukun.Domin.Entities;
using Sukun.Application.Dtos.RemembranceContent.Response;
using Sukun.Application.Interfaces;

namespace Sukun.Application.Implemantation
{
    public class SourceResolverService : ISourceResolverService
    {
        private readonly IQuranRepository _quranRepo;
        private readonly IHadithRepository _hadithRepo;
        private readonly IAsmaulHusnaRepository _asmaulHusnaRepo;
        private readonly IDuaItemRepository _duaRepo;
        private readonly ITasbihRepository _tasbihRepo;
        private readonly IUnitOfWork _unitOfWork;

        public SourceResolverService(
            IQuranRepository quranRepo,
            IHadithRepository hadithRepo,
            IAsmaulHusnaRepository asmaulHusnaRepo,
            IDuaItemRepository duaRepo,
            ITasbihRepository tasbihRepo,
            IUnitOfWork unitOfWork)
        {
            _quranRepo = quranRepo;
            _hadithRepo = hadithRepo;
            _asmaulHusnaRepo = asmaulHusnaRepo;
            _duaRepo = duaRepo;
            _tasbihRepo = tasbihRepo;
            _unitOfWork = unitOfWork;
        }
        private const int MaxVersesForSurah = 20; 

        public async Task<Dictionary<Guid, SourcePreviewDto>> ResolveAsync(
            IEnumerable<RemembranceContent> contents)
        {
            var result = new Dictionary<Guid, SourcePreviewDto>();
            var activeContents = contents.Where(c => !c.IsDeleted).ToList();

            if (!activeContents.Any()) return result;

            var verseContents = activeContents
                .Where(c => c.SourceType == SourceType.QuranVerse && c.SourceId.HasValue)
                .ToList();

            if (verseContents.Any())
            {
                var verseIds = verseContents.Select(c => c.SourceId!.Value).ToList();
                var verses = await _unitOfWork.Repository<QuranVerse>().AsQueryable()
                    .Where(v => verseIds.Contains(v.Id))
                    .Include(v => v.Surah)
                    .ToListAsync();

                foreach (var v in verses)
                {
                    result[v.Id] = new SourcePreviewDto
                    {
                        DisplayText = v.Text,
                        Reference = v.Surah != null
                            ? $"سورة {v.Surah.Name} - الآية {v.VerseNumber}"
                            : $"الآية {v.VerseNumber}"

                    };
                }
            }

            var surahContents = activeContents
                .Where(c => c.SourceType == SourceType.QuranSurah && c.SourceId.HasValue)
                .ToList();

            if (surahContents.Any())
            {
                var surahIds = surahContents.Select(c => c.SourceId!.Value).ToList();

                var surahs = await _unitOfWork.Repository<QuranSurah>().AsQueryable()
                    .Where(s => surahIds.Contains(s.Id))
                    .Include(s => s.Verses.OrderBy(v => v.VerseNumber).Take(MaxVersesForSurah))
                    .ToListAsync();

                foreach (var surah in surahs)
                {
                    var verses = surah.Verses.Take(MaxVersesForSurah).ToList();

                    if (verses.Any())
                    {
                        var fullText = string.Join($"{{}}", verses.OrderBy(v => v.VerseNumber).Select(v => v.Text));
                        var verseRange = verses.Count == 1
                            ? $"الآية {verses.First().VerseNumber}"
                            : $"الآيات 1-{verses.Count}";

                        result[surah.Id] = new SourcePreviewDto
                        {
                            DisplayText = fullText,
                            Reference = $" {surah.Name} ({verseRange})"
                        };
                    }
                    else
                    {
                        result[surah.Id] = new SourcePreviewDto
                        {
                            DisplayText = "[لا توجد آيات في هذه السورة]",
                            Reference = surah.Name
                        };
                    }
                }
            }

            var hadithContents = activeContents
                .Where(c => c.SourceType == SourceType.Hadith && c.SourceId.HasValue)
                .ToList();

            if (hadithContents.Any())
            {
                var hadithIds = hadithContents.Select(c => c.SourceId!.Value).ToList();

                var hadiths = await _hadithRepo.AsQueryable()
                    .Where(h => hadithIds.Contains(h.Id))
                    .Include(h => h.Book)
                    .ToListAsync();

                foreach (var h in hadiths)
                {
                    result[h.Id] = new SourcePreviewDto
                    {
                        DisplayText = h.Text,
                        Reference = string.Join(" - ", new[]
                        {
                            h.Book?.Name,
                            h.HadithNumber,
                            h.Reference
                        }.Where(s => !string.IsNullOrWhiteSpace(s))),
                    };
                }
            }

            var allahNameContents = activeContents
                .Where(c => c.SourceType == SourceType.AllahName && c.SourceId.HasValue)
                .ToList();

            if (allahNameContents.Any())
            {
                var nameIds = allahNameContents.Select(c => c.SourceId!.Value).ToList();

                var names = await _asmaulHusnaRepo.FindAsync(n => nameIds.Contains(n.Id));

                foreach (var n in names)
                {
                    result[n.Id] = new SourcePreviewDto
                    {
                        DisplayText = n.NameArabic,
                        Reference = $"الاسم {n.Number} من أسماء الله الحسنى",
                        DisplayTextEn = n.NameTransliteration,
                        MeaningEn = n.MeaningEnglish,
                        Meaning = n.MeaningArabic
                    };
                }
            }

            var duaContents = activeContents
                .Where(c => c.SourceType == SourceType.Dua && c.SourceId.HasValue)
                .ToList();

            if (duaContents.Any())
            {
                var duaIds = duaContents.Select(c => c.SourceId!.Value).ToList();

                var duas = await _duaRepo.FindAsync(d => duaIds.Contains(d.Id));

                foreach (var d in duas)
                {
                    result[d.Id] = new SourcePreviewDto
                    {
                        DisplayText = d.Text,
                        DisplayTextEn = d.TextEn,
                        Reference = d.Reference ?? d.Title,
                        Benefits = d.Virtue
                    };
                }
            }

            var tasbihContents = activeContents
                .Where(c => c.SourceType == SourceType.Tasbih && c.SourceId.HasValue)
                .ToList();

            if (tasbihContents.Any())
            {
                var tasbihIds = tasbihContents.Select(c => c.SourceId!.Value).ToList();

                var tasbihat = await _tasbihRepo.FindAsync(t => tasbihIds.Contains(t.Id));

                foreach (var t in tasbihat)
                {
                    //var content = tasbihContents.First(c => c.SourceId == t.Id);

                    result[t.Id] = new SourcePreviewDto
                    {
                        DisplayText = t.Title,
                        DisplayTextEn = t.TitleEn,
                        Reference = t.Reference,
                        Benefits = t.Benefits
                    };
                }
            }

            return result;
        }
    }
}
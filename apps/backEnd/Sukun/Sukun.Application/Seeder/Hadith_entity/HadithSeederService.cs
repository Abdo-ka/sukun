using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;
using Sukun.Infrastructure.InfrastructureBases;
using System.Net.Http.Json;
using System.Text.Json;

namespace Sukun.Application.Seeder.Hadith_entity
{

    public class HadithSeederService : IHadithSeederService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HadithApiOptions _apiOptions;
        private readonly ILogger<HadithSeederService> _logger;

        public HadithSeederService(
            IUnitOfWork unitOfWork,
            IHttpClientFactory httpClientFactory,
            IOptions<HadithApiOptions> apiOptions,
            ILogger<HadithSeederService> logger)
        {
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
            _apiOptions = apiOptions.Value;
            _logger = logger;
        }

        public async Task SeedHadithsAsync()
        {
            if (string.IsNullOrEmpty(_apiOptions.Key))
            {
                _logger.LogError("Hadith API Key is missing");
                return;
            }

            var client = _httpClientFactory.CreateClient();
            var apiKey = _apiOptions.Key;

            var bookRepo = _unitOfWork.Repository<IslamicBook>();
            var sectionRepo = _unitOfWork.Repository<IslamicBookSection>();
            var hadithRepo = _unitOfWork.Repository<Hadith>();

            var existingHadiths = await hadithRepo.CountAsync();
            if (existingHadiths > 5000)
            {
                _logger.LogInformation("Hadiths already seeded ({Count}), skipping.", existingHadiths);
                return;
            }

            _logger.LogInformation("Starting Hadith seeding from hadithapi.com...");

            // جلب الكتب
            var booksUrl = $"https://hadithapi.com/api/books?apiKey={apiKey}";
            var booksResponse = await client.GetFromJsonAsync<ApiBooksResponse>(booksUrl);

            if (booksResponse == null || booksResponse.Books == null)
            {
                _logger.LogError("Failed to fetch books");
                return;
            }

            int totalAdded = 0;

            foreach (var apiBook in booksResponse.Books)
            {
                // إنشاء أو جلب الكتاب
                var book = await bookRepo.FirstOrDefaultAsync(b => b.Name == apiBook.BookName);
                if (book == null)
                {
                    book = new IslamicBook
                    {
                        Id = Guid.NewGuid(),
                        Name = apiBook.BookName,
                        NameAr = apiBook.BookName,
                        Author = apiBook.WriterName,
                        Type = BookType.Hadith,
                        CreateAt = DateTime.UtcNow
                    };
                    await bookRepo.AddAsync(book);
                    await _unitOfWork.CompleteAsync();
                }

                // جلب الفصول
                var chaptersUrl = $"https://hadithapi.com/api/{apiBook.BookSlug}/chapters?apiKey={apiKey}";
                var chaptersResponse = await client.GetFromJsonAsync<ApiChaptersResponse>(chaptersUrl);

                if (chaptersResponse == null || chaptersResponse.Chapters == null) continue;

                foreach (var apiChapter in chaptersResponse.Chapters)
                {
                    var section = await sectionRepo.FirstOrDefaultAsync(s => s.BookId == book.Id && s.Name == apiChapter.ChapterArabic);
                    if (section == null)
                    {
                        section = new IslamicBookSection
                        {
                            Id = Guid.NewGuid(),
                            BookId = book.Id,
                            NameEn= apiChapter.ChapterEnglish,
                            Name = apiChapter.ChapterArabic,
                            CreateAt = DateTime.UtcNow,
                            ChapterNumber = apiChapter.ChapterNumber,
                        };
                        await sectionRepo.AddAsync(section);
                        await _unitOfWork.CompleteAsync();
                    }

                    // جلب الأحاديث في الفصل (pagination)
                    int page = 1;
                    bool hasMore = true;

                    while (hasMore)
                    {
                        var hadithsUrl = $"https://hadithapi.com/api/hadiths?book={apiBook.BookSlug}&chapter={apiChapter.ChapterNumber}?page={page}&paginate=100&apiKey={apiKey}";
                        var hadithsResponse = await client.GetFromJsonAsync<ApiHadithsResponse>(hadithsUrl);

                        if (hadithsResponse == null || hadithsResponse.Hadiths?.Data == null || !hadithsResponse.Hadiths.Data.Any())
                        {
                            hasMore = false;
                            break;
                        }

                        foreach (var apiHadith in hadithsResponse.Hadiths.Data)
                        {
                            var exists = await hadithRepo.ExistsAsync(h => h.BookId == book.Id && h.HadithNumber == apiHadith.HadithNumber && h.SectionId == section.Id);
                            if (exists) continue;

                            var hadith = new Hadith
                            {
                                Id = Guid.NewGuid(),
                                BookId = book.Id,
                                SectionId = section.Id,
                                HadithNumber = apiHadith.HadithNumber,
                                Text = apiHadith.HadithArabic,
                                Grade = ParseGrade(apiHadith.Status),
                                CreateAt = DateTime.UtcNow,
                                Reference = apiHadith.Book.BookName,
                                HeadingArabic = apiHadith.HeadingArabic

                            };

                            await hadithRepo.AddAsync(hadith);
                            totalAdded++;
                        }
                        await _unitOfWork.CompleteAsync();

                        page++;
                        if (page > hadithsResponse.Hadiths.Last_page) hasMore = false;
                    }
                }
            }

            _logger.LogInformation("Hadith seeding completed. Added {Count} hadiths from hadithapi.com.", totalAdded);
        }
        private HadithGrade ParseGrade(string grade)
        {
            return grade.ToLower() switch
            {
                "sahih" => HadithGrade.Sahih,
                "hasan" => HadithGrade.Hasan,
                "daif" => HadithGrade.Daif,
                _ => HadithGrade.Unknown
            };
        }
    }
}


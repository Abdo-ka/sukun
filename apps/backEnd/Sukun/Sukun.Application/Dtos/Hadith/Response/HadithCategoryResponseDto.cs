namespace Sukun.Application.Dtos.Hadith.Response
{
    public class HadithCategoryResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Icon { get; set; }
        public int Order { get; set; }
        public int HadithsCount { get; set; } // عدد الأحاديث في الفئة (مفيد للعرض)
    }

}

namespace Sukun.Application.Dtos.RemembranceCategory.Response
{
    public class RemembranceCategoryResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? NameEn { get; set; }
        public int RemembrancesCount { get; set; }
    }

}

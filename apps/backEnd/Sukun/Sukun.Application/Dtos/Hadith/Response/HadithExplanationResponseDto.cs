namespace Sukun.Application.Dtos.Hadith.Response
{
    public class HadithExplanationResponseDto
    {
        public Guid Id { get; set; }
        public string Scholar { get; set; } = string.Empty;
        public string Explanation { get; set; } = string.Empty;
    }

}

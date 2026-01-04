namespace Sukun.Application.Dtos.Hadith.Request
{
    public class HadithExplanationUpdateDto
    {
        public Guid? Id { get; set; }
        public string Scholar { get; set; } = string.Empty;
        public string Explanation { get; set; } = string.Empty;
    }

}

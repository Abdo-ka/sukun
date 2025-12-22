namespace Sukun.Application.Dtos.Hadith.Response
{
    public class HadithResponseDto : HadithListResponseDto
    {
        public string? GradedBy { get; set; }
        public string? GradeExplanation { get; set; }
        public List<HadithExplanationResponseDto> Explanations { get; set; } = new();
    }

}

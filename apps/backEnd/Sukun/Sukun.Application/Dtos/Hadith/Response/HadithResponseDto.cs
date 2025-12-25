namespace Sukun.Application.Dtos.Hadith.Response
{
    public class HadithResponseDto : HadithListResponseDto
    {
        public List<HadithExplanationResponseDto> Explanations { get; set; } = new();
    }

}

namespace Sukun.Application.Dtos.Narrative.Request
{
    public class NarrativeSectionUpdateDto
    {
        public Guid? Id { get; set; } // للتعديل أو الحذف
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int? DisplayOrder { get; set; }
        public string? MediaUrl { get; set; }
        public bool? IsDeleted { get; set; } // للحذف الناعم داخل القسم
    }
}

using Sukun.Domin.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukun.Application.Dtos.Hadith.Request
{

    public class HadithUpdateDto 
    {
        public Guid? CategoryId { get; set; }
        public Guid? BookId { get; set; }
        public Guid? SectionId { get; set; }
        public string Reference { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public HadithGrade Grade { get; set; }
        public string? HadithNumber { get; set; }
        public List<HadithExplanationUpdateDto>? Explanations { get; set; }
    }

}

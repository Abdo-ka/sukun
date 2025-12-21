using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukun.Application.Dtos.AsmaulHusna.Responce
{
    public class AsmaulHusnaResponseDto
    {
        public Guid Id { get; set; }
        public int Number { get; set; } // 1 إلى 99
        public string NameArabic { get; set; } = string.Empty; // الاسم بالعربية
        public string NameTransliteration { get; set; } = string.Empty; // الاسم مكتوب باللاتينية (مثل Ar-Rahman)
        public string MeaningArabic { get; set; } = string.Empty; // المعنى بالعربية
        public string MeaningEnglish { get; set; } = string.Empty; // المعنى بالإنجليزية (اختياري)
    }
}

using Sukun.Application.Dtos.IslamicBookSection.Response;
using Sukun.Domin.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukun.Application.Dtos.IslamicBook.Response
{
    public class IslamicBookResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string? NameEn { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public BookType Type { get; set; }
        public int Order { get; set; }
        public int HadithsCount { get; set; }
        public int SectionsCount { get; set; }
        public List<IslamicBookSectionResponseDto> Sections { get; set; } = new();
    }
}

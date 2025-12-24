using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sukun.Application.Dtos.RemembranceContent.Response;

namespace Sukun.Application.Dtos.Remembrance.Response
{
    public class RemembranceResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int RecommendedCount { get; set; }
        public string? Benefits { get; set; }
        public bool IsDaily { get; set; }
        public List<string> CategoryNames { get; set; } = new(); // أسماء الفئات
        public List<RemembranceContentResponseDto> Contents { get; set; } = new();
    }

}

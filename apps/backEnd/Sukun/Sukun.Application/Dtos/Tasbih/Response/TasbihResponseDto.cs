using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukun.Application.Dtos.Tasbih.Response
{
    public class TasbihResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string? Benefits { get; set; }
        public int RecommendedCount { get; set; }
        public int Order { get; set; }
    }
}

using Sukun.Domin.Entities;
using Sukun.Domin.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukun.Application.Dtos.Narrative.Response
{

    public class NarrativeListResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? TitleAr { get; set; }
        public ContentType Type { get; set; }
        public string? ShortDescription { get; set; }
        public bool IsFeatured { get; set; }
        public int ViewCount { get; set; }
    }
}

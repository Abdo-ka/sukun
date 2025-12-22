using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukun.Application.Seeder.Hadith_entity
{

    // Classes للـ Deserialize (حسب هيكل الـ API)
    public class HadithApiResponse
        {
            public List<ApiHadith> Hadiths { get; set; } = new();
        }

        public class ApiHadith
        {
            public int HadithNumber { get; set; }
            public string? Text { get; set; }
            public string? Grade { get; set; }
            public string? Explanation { get; set; } // إذا كان موجودًا في المصدر
        }
    
}


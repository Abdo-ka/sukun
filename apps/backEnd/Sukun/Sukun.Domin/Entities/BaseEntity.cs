using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukun.Domin.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; } 
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}

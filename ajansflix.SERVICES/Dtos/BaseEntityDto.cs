using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Dtos
{
    public class BaseEntityDto
    {
        public int Id{ get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime UpdatedTime { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
    }
}

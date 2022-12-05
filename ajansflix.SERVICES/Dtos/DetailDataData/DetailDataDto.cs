using ajansflix.SERVICES.Dtos.DetailData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Dtos.DetailDataData
{
    public class DetailDataDto : BaseEntityDto
    {
        public string DataName { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public bool IsLock { get; set; }
        public int DetailId { get; set; }

        public DetailDto detail { get; set; }
    }
}

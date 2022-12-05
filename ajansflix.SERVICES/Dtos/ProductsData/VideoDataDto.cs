using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Dtos.ProductsData
{
    public class VideoDataDto : BaseEntityDto
    {
        public string VideoPath { get; set; }
        public string ProductId { get; set; }
        public ProductDto product { get; set; }
        
    }
}

using ajansflix.SERVICES.Dtos.ProductDetailsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Dtos.DetailData
{
    public class DetailDto : BaseEntityDto
    {
        public string DetailName { get; set; }
        public string Type { get; set; }
        public virtual List<ProductDetailDto> productDetails { get; set; }
    }
}

using ajansflix.SERVICES.Dtos.DetailData;
using ajansflix.SERVICES.Dtos.OrdersData;
using ajansflix.SERVICES.Dtos.ProductsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Dtos.ProductDetailsData
{
    public class ProductDetailDto : BaseEntityDto
    {
        public int ProductId { get; set; }
        public int DetailId { get; set; }
        public int Sorted { get; set; }

        public ProductDto product { get; set; }
        public DetailDto detail { get; set; }
    }
}

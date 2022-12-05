using ajansflix.SERVICES.Dtos.OrdersData;
using ajansflix.SERVICES.Dtos.ProductsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Dtos.OrderProductsData
{
    public class OrderProductDto : BaseEntityDto
    {
        public decimal Price { get; set; }

        public int ProductId { get; set; }

        public int OrderId { get; set; }

        public ProductDto product { get; set; }
        public OrdersDto order { get; set; }
    }
}

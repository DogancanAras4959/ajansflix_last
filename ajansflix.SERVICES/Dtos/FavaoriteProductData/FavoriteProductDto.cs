using ajansflix.SERVICES.Dtos.CustomerData;
using ajansflix.SERVICES.Dtos.ProductsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Dtos.FavaoriteProductData
{
    public class FavoriteProductDto : BaseEntityDto
    {

        public int ProductId { get; set; }
        public int CustomerId { get; set; }

        public ProductDto product { get; set; }
        public CustomerDto customer { get; set; }
    }
}

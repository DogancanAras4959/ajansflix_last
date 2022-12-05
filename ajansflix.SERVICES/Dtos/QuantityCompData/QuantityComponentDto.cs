using ajansflix.SERVICES.Dtos.ProductsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Dtos.QuantityCompData
{
    public class QuantityComponentDto : BaseEntityDto
    {
        public int CompQuantity { get; set; }
        public int CompPrice { get; set; }
        public int ProductId { get; set; }
        public DateTime CompDeadLine { get; set; }
        public ProductDto product { get; set; }
    }
}

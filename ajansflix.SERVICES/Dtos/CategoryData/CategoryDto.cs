using ajansflix.SERVICES.Dtos.ProductsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Dtos.CategoryData
{
    public class CategoryDto : BaseEntityDto
    {
        public string CategoryName { get; set; }
        public string CategoryImage { get; set; }
        public string CategoryDescription { get; set; }
        public string CategoryStatus { get; set; }
        public string StatusCode { get; set; }
        public int CategorySorted { get; set; }

        public List<ProductDto> products { get; set; }
    }
}

using ajansflix.DOMAIN.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.DOMAIN.Models
{
    [Table("productDetails")]
    public class ProductDetails : BaseEntity
    {
        public ProductDetails()
        {

        }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

        [ForeignKey("detail")]
        public int DetailId { get; set; }
        public int Sorted { get; set; }

        public Products product { get; set; }
        public Details detail { get; set; }
    }
}

using ajansflix.DOMAIN.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.DOMAIN.Models
{
    [Table("orderProducts")]
    public class OrderProducts : BaseEntity
    {
        public OrderProducts()
        {

        }

        public decimal Price { get; set; }

        [ForeignKey("product")]
        public int ProductId { get; set; }

        [ForeignKey("order")]
        public int OrderId { get; set; }

        public Products product { get; set; }
        public Orders order { get; set; }
    }
}

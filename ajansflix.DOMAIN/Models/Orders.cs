using ajansflix.DOMAIN.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.DOMAIN.Models
{
    [Table("orders")]
    public class Orders : BaseEntity
    {
        public Orders()
        {
            orderProducts = new List<OrderProducts>();
        }

        public string OrderNo { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }


        [ForeignKey("orderStatus")]
        public int StatusId { get; set; }

        [ForeignKey("customer")]
        public int CustomerId { get; set; }

        public string OrderDetailHtml { get; set; }

        public Customers customer {get;set; }
        public OrderStatus orderStatus { get; set; }

        public List<OrderProducts> orderProducts { get; set; }
    }
}

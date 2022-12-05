using ajansflix.DOMAIN.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.DOMAIN.Models
{
    [Table("orderStatus")]
    public class OrderStatus : BaseEntity
    {
        public OrderStatus()
        {
            orderList = new List<Orders>();
        }

        public string StatusName { get; set; }

        public ICollection<Orders> orderList { get; set; }
    }
}

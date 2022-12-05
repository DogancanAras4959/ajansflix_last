using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Dtos.OrdersData
{
    public class OrderStatusDto : BaseEntityDto
    {
        public string StatusName { get; set; }
        public List<OrdersDto> orderList { get; set; }
    }
}

using ajansflix.SERVICES.Dtos.CustomerData;
using ajansflix.SERVICES.Dtos.OrderProductsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Dtos.OrdersData
{
    public class OrdersDto : BaseEntityDto
    {
        public string OrderNo { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
        public int StatusId { get; set; }
        public int CustomerId { get; set; }
        public string OrderDetailHtml { get; set; }

        public OrderStatusDto orderStatus { get; set; }
        public CustomerDto customer { get; set; }
        public List<OrderProductDto> orderProducts { get; set; }
    }
}

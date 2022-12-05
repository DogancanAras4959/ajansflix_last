using ajansflix.DOMAIN.Models;
using ajansflix.SERVICES.Crud;
using ajansflix.SERVICES.Dtos.OrdersData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Interface
{
    public interface IOrderService : ICrudService<Orders, OrdersDto>
    {
        List<OrdersDto> listOrderWithCustomer();
        int TotalCountPaid();
        int TotalCountUnpaid();
        int TotalCountCancel();
        int TotalCount();

    }
}

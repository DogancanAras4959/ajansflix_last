using ajansflix.DOMAIN.Models;
using ajansflix.SERVICES.Crud;
using ajansflix.SERVICES.Dtos.OrderProductsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Interface
{
    public interface IOrderProductsService : ICrudService<OrderProducts, OrderProductDto>
    {
        List<OrderProductDto> orderProductsByOrdersAndProduct(int orderId);
    }
}

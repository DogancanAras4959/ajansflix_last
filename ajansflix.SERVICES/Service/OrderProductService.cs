using ajansflix.CORE.Repository;
using ajansflix.DOMAIN.Models;
using ajansflix.SERVICES.Crud;
using ajansflix.SERVICES.Dtos.OrderProductsData;
using ajansflix.SERVICES.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Service
{
    public class OrderProductService : CrudService<OrderProducts, OrderProductDto>, IOrderProductsService
    {
        public OrderProductService(IRepository<OrderProducts> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public List<OrderProductDto> orderProductsByOrdersAndProduct(int orderId)
        {
            var entity = _repository.Where(x => x.OrderId == orderId).Include("order").Include("products").AsNoTracking().ToList();
            var entityDto = _mapper.Map<List<OrderProducts>, List<OrderProductDto>>(entity);
            return entityDto;
        }
    }
}

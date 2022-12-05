using ajansflix.CORE.Repository;
using ajansflix.DOMAIN.Models;
using ajansflix.SERVICES.Crud;
using ajansflix.SERVICES.Dtos.OrdersData;
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
    public class OrderService : CrudService<Orders, OrdersDto>, IOrderService
    {
        public OrderService(IRepository<Orders> repository, IMapper mapper) : base(repository, mapper)
        {
                
        }

        public List<OrdersDto> listOrderWithCustomer()
        {
            var orders = _repository.Where(x => x.Id > 0).Include("customer").Include("orderStatus").OrderByDescending(x => x.CreatedTime).ToList();
            var ordersDto = _mapper.Map<List<Orders>, List<OrdersDto>>(orders);
            return ordersDto;
        }

        public int TotalCount()
        {
            int totalCount = (int)_repository.Where(x => x.Id > 0).Sum(x => x.TotalPrice);
            return totalCount;
        }

        public int TotalCountCancel()
        {
            int totalCount = (int)_repository.Where(x => x.StatusId == 3).Sum(x => x.TotalPrice);
            return totalCount;
        }

        public int TotalCountPaid()
        {
            int totalCount = (int)_repository.Where(x => x.StatusId == 1).Sum(x => x.TotalPrice);
            return totalCount;
        }

        public int TotalCountUnpaid()
        {
            int totalCount = (int)_repository.Where(x => x.StatusId == 2).Sum(x => x.TotalPrice);
            return totalCount;
        }
    }
}

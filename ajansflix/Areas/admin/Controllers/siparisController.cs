using ajansflix.Areas.admin.Data;
using ajansflix.SERVICES.Dtos.OrdersData;
using ajansflix.SERVICES.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ajansflix.Areas.admin.Controllers
{
    [Area("admin")]
    public class siparisController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderProductsService _orderProductService;
        private readonly ICustomerService _customerService;
        private readonly IOrderStatusService _orderStatusService;
        public siparisController(IOrderService orderService, IOrderProductsService orderProductService, ICustomerService customerService, IOrderStatusService ordersStatusService)
        {
            _orderStatusService = ordersStatusService;
            _orderService = orderService;
            _orderProductService = orderProductService;
            _customerService = customerService;
        }

        [CheckLogin]
        public IActionResult siparisler()
        {
            var orders = _orderService.listOrderWithCustomer();
            ViewBag.Total = _orderService.TotalCount();
            return View(orders);
        }

        [CheckLogin]
        public IActionResult siparisdetay(int Id)
        {
            var orders = _orderService.Get(Id);
            var orderProductList = _orderProductService.orderProductsByOrdersAndProduct(orders.Id);
            orders.orderProducts = orderProductList;
            return View(orders);
        }

        [CheckLogin]
        public IActionResult siparissil(int Id)
        {
            var orders = _orderService.Get(Id);
            _orderService.Delete(orders.Id);
            return Redirect("/admin/siparis/siparisler");
        }

        [CheckLogin]
        [HttpGet]
        public IActionResult siparisduzenle(int Id)
        {
            var orders = _orderService.Get(Id);
            ViewBag.Customers = new SelectList(_customerService.GetAll(), "Id", "NameSurname");
            ViewBag.Status = new SelectList(_orderStatusService.GetAll(), "Id", "StatusName");
            return View(orders);
        }

        [HttpPost]
        public IActionResult siparisguncelle(OrdersDto order)
        {
            var orderGet = _orderService.Get(order.Id);
            if(orderGet != null)
            {
                orderGet.CustomerId = order.CustomerId;
                orderGet.OrderNo = order.OrderNo;
                orderGet.IsActive = order.IsActive;
                orderGet.Quantity = order.Quantity;
                orderGet.StatusId = order.StatusId;
                orderGet.TotalPrice = order.TotalPrice;
                orderGet.UpdatedTime = DateTime.Now;
                _orderService.Update(orderGet);

                return Redirect("/admin/siparis/siparisduzenle?Id=" + order.Id);
            }
            else
            {
                return Redirect("/admin/siparis/siparisler");

            }
        }
    }
}

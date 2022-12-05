using ajansflix.CORE.CartModels;
using ajansflix.SERVICES.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ajansflix.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        public HeaderViewComponent(IProductService prodcutService)
        {
            _productService = prodcutService;
        }

        public IViewComponentResult Invoke()
        {
            BindDatabaseData();
            return View();
        }

        #region Extern
      
        public void BindDatabaseData()
        {
            var productList = _productService.listProductToWebForSearch();
            ViewBag.ProductList = productList;

            IsHaveCookieCart();

        }

        public void IsHaveCookieCart()
        {
            var cookieCart = Request.Cookies["CartItems"];
            decimal total = 0;

            if (cookieCart != null)
            {
                List<CartItem> cart = JsonSerializer.Deserialize<List<CartItem>>(cookieCart);
                List<ComponentItem> component = new List<ComponentItem>();

                if (cart != null)
                {
                    
                    foreach (var item in cart)
                    {
                        if(item._compItems != null)
                        {
                            component.AddRange(item._compItems);
                        }
                    }
                 
                    total = Convert.ToInt32(cart.Sum(x => x.BasePrice));
                    ViewBag.Total = total;
                    ViewBag.Cart = cart;
                    ViewBag.CartSerilize = JsonSerializer.Serialize(cart);
                }
            }
        }

        #endregion
    }
}

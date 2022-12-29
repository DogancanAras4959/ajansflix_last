using ajansflix.CORE.CartModels;
using ajansflix.CORE.EmailConfig;
using ajansflix.Models;
using ajansflix.SERVICES.Dtos.CustomerData;
using ajansflix.SERVICES.Dtos.DetailDataData;
using ajansflix.SERVICES.Dtos.OrdersData;
using ajansflix.SERVICES.Dtos.ProductsData;
using ajansflix.SERVICES.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ajansflix.Controllers
{
    public class hizmetController : Controller
    {

        #region Fields

        private readonly IProductService _productService;
        private readonly IDetailService _detailService;
        private readonly IDetailDataService _detailDataService;
        private readonly IProductDetailService _productDetailService;
        private readonly IImageService _imageService;
        private readonly IEmailSender _emailSender;
        private readonly ICategoryService _categoryService;
        private readonly IOrderService _orderService;
        private readonly IOrderStatusService _orderStatusService;
        private readonly IOrderProductsService _orderProductService;
        private readonly ICustomerService _customerService;
        public hizmetController(IProductService productService, IDetailService detailService, IDetailDataService detailDataService, IProductDetailService productDetailService, IImageService imageService, IEmailSender emailSender, ICategoryService categoryService,
            IOrderService orderService, IOrderStatusService orderStatusService, IOrderProductsService orderProductService, ICustomerService customerService)
        {
            _productService = productService;
            _detailDataService = detailDataService;
            _detailService = detailService;
            _productDetailService = productDetailService;
            _imageService = imageService;
            _emailSender = emailSender;
            _categoryService = categoryService;
            _orderService = orderService;
            _orderStatusService = orderStatusService;
            _orderProductService = orderProductService;
            _customerService = customerService;
        }

        #endregion

        [HttpGet("detay/{Id}/{productname}", Name = "detay")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> detay(int Id, string productname)
        {

            #region Data and Product Detail

            var product = _productService.getProduct(Id);

            DataLoad(product.Id, product);

            string friendlyTitle = productname;

            if (!string.Equals(friendlyTitle, productname, StringComparison.Ordinal))
            {
                return this.RedirectToRoutePermanent("detay", new { id = Id, productname = friendlyTitle });
            }

            ViewBag.ImagesGallery = await _imageService.GetAllImagesByProductId(product.Id);

            #endregion

            #region Relational

            var relationalProducts = _productService.listProductToRelationalByCategory(Id, product.CategoryId);
            ViewBag.ProductsRelational = relationalProducts;

            #endregion

            #region Meta
            MetaViewModel meta = new()
            {
                Image = "https://reklamlutfen.com" + product.ImagePath,
                Title = product.ProductMetaName,
                ogDescription = product.ProductMetaDescription,
                Description = product.ProductMetaDescription,
                ogTitle = product.ProductMetaName,
                Url = "https://reklamlutfen.com/detay/" + product.Id + "/" + product.GenerateSlug()
            };

            ViewBag.Meta = meta;

            #endregion


            return View(product);
        }

        [Route("hizmetler")]
        public IActionResult hizmetler(int id)
        {
            Meta("Reklam Lütfen | Hizmetler");
            var category = _categoryService.Get(id);

            var productList = _productService.productListByCategory(category.Id);
            ViewBag.ProductList = productList;

            return View(category);
        }

        [Route("sepet")]
        public IActionResult sepet()
        {
            Meta("Reklam Lüften | Sepet");
            var cookieCart = Request.Cookies["CartItems"];
            decimal total = 0;

            if (cookieCart != null)
            {
                List<CartItem> cart = JsonSerializer.Deserialize<List<CartItem>>(cookieCart);

                if (cart != null)
                {
                    total = Convert.ToInt32(cart.Sum(x => x.BasePrice));
                    ViewBag.Total = total;
                    ViewBag.Cart = cart;
                }
                return View();
            }
            else
            {
                return RedirectToAction("sayfa", "anasayfa");
            }

        }

        [HttpPost]
        public JsonResult sepetekle(CartItem cartItem, string cartData)
        {
            try
            {
                var optionsAccess = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    NumberHandling = JsonNumberHandling.AllowReadingFromString
                };

                List<ComponentItem> compItems = JsonSerializer.Deserialize<List<ComponentItem>>(cartData, optionsAccess);

                foreach (var item in compItems)
                {
                    ComponentItem compItem = new()
                    {
                        CompName = item.CompName,
                        CompValue = item.CompValue,
                        Price = item.Price,
                        ProductId = item.ProductId,
                        CompId = item.CompId,
                        BaseId = item.BaseId
                    };

                    cartItem._compItems.Add(compItem);
                }

                var cookie = Request.Cookies["CartItems"];

                if (cookie != null)
                {
                    List<CartItem> cartItemsInCookie = JsonSerializer.Deserialize<List<CartItem>>(cookie);

                    List<CartItem> newCartItemList = new();
                    newCartItemList.AddRange(cartItemsInCookie);

                    CartItem findCart = newCartItemList.Find(x => x.Item == cartItem.Item);           

                    if (findCart == null)
                    {
                        newCartItemList.Add(cartItem);
                    }

                    else
                    {
                        findCart.BasePrice = cartItem.BasePrice;
                        foreach (var item2 in newCartItemList)
                        {
                            if (item2.Id == cartItem.Id)
                            {
                                item2._compItems = compItems;
                            }
                        }
                    }

                    string newCartList = JsonSerializer.Serialize(newCartItemList);
                    Response.Cookies.Append("CartItems", newCartList);
                }
                else
                {
                    List<CartItem> cartItems = new();
                    cartItems.Add(cartItem);

                    CookieOptions options = new();
                    options.Expires = DateTime.Now.AddYears(1);
                    string cartListCookie = JsonSerializer.Serialize(cartItems);
                    Response.Cookies.Append("CartItems", cartListCookie, options);
                }

                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }

        }

        [HttpPost]
        public JsonResult sepetekleHome(CartItem cartHome)
        {
            try
            {
                var optionsAccess = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    NumberHandling = JsonNumberHandling.AllowReadingFromString
                };

                var cookie = Request.Cookies["CartItems"];

                if (cookie != null)
                {
                    List<CartItem> cartItemsInCookie = JsonSerializer.Deserialize<List<CartItem>>(cookie);

                    List<CartItem> newCartItemList = new();
                    newCartItemList.AddRange(cartItemsInCookie);

                    CartItem findCart = newCartItemList.Find(x => x.Item == cartHome.Item);

                    if (findCart == null)
                    {
                        newCartItemList.Add(cartHome);
                    }

                    else
                    {
                        findCart.BasePrice = cartHome.BasePrice;
                    }

                    string newCartList = JsonSerializer.Serialize(newCartItemList);
                    Response.Cookies.Append("CartItems", newCartList);
                }
                else
                {
                    List<CartItem> cartItems = new();
                    cartItems.Add(cartHome);

                    CookieOptions options = new();
                    options.Expires = DateTime.Now.AddYears(1);
                    string cartListCookie = JsonSerializer.Serialize(cartItems);
                    Response.Cookies.Append("CartItems", cartListCookie, options);
                }

                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }

        }

        [Route("odeme")]
        public IActionResult odeme()
        {
            Meta("Reklam Lütfen | Ödeme");
            var cookieCart = Request.Cookies["CartItems"];
            decimal total = 0;

            if (cookieCart != null)
            {
                List<CartItem> cart = JsonSerializer.Deserialize<List<CartItem>>(cookieCart);

                if (cart != null)
                {

                    total = Convert.ToInt32(cart.Sum(x => x.BasePrice));
                    ViewBag.Total = total;
                    ViewBag.Cart = cart;
                }
                return View();
            }
            else
            {
                return RedirectToAction("sayfa", "anasayfa");
            }
          
        }

        [Route("favori")]
        public IActionResult favoriler()
        {
            Meta("Reklam Lütfen | Favori Ürünler");
            return View();
        }

        [HttpPost]
        public JsonResult secimleriTemizle(CartItem cartItem)
        {

            var cookie = Request.Cookies["CartItems"];

            if (cookie != null)
            {
                List<CartItem> cartItemsInCookie = JsonSerializer.Deserialize<List<CartItem>>(cookie);

                List<CartItem> newCartItemList = new();
                newCartItemList.AddRange(cartItemsInCookie);

                CartItem findCart = newCartItemList.Find(x => x.Item == cartItem.Item);

                if (findCart == null)
                {
                    newCartItemList.Add(cartItem);
                }

                findCart.BasePrice -= findCart._compItems.Sum(x=> x.Price);
                findCart._compItems = null;

                string newCartList = JsonSerializer.Serialize(newCartItemList);
                Response.Cookies.Append("CartItems", newCartList);
            }
            else
            {
                List<CartItem> cartItems = new();
                cartItems.Add(cartItem);

                CookieOptions options = new();
                options.Expires = DateTime.Now.AddYears(1);
                string cartListCookie = JsonSerializer.Serialize(cartItems);
                Response.Cookies.Append("CartItems", cartListCookie, options);
            }

            return Json(true);

        }

        #region Extern

        [HttpGet]
        public JsonResult bilesengetir(int id, ProductDto product)
        {
            var response = _detailDataService.getDataObject(id);

            ComponentItem item = new()
            {
                CompName = response.detail.DetailName,
                CompValue = response.DataName,
                Price = response.Price,
                ProductId = product.Id,
                CompId = response.DetailId,
            };

            return Json(item);
        }

        public void DataLoad(int Id, ProductDto product)
        {
            bool isCartIn = false;
            List<CartItem> cart = null;
            var cookieCart = Request.Cookies["CartItems"];
          
            if(cookieCart != null)
            {
                cart = JsonSerializer.Deserialize<List<CartItem>>(cookieCart);

                foreach (var item in cart)
                {
                    if (item.Id == Id)
                    {
                        isCartIn = true;
                    }
                    break;
                }
            }
           
            var detailsInProduct = _productDetailService.listDetails(Id);
            ViewBag.Details = detailsInProduct;

            List<DetailDataDto> datas = new();
            List<ComponentItem> itemsPriceFree = new();

            foreach (var item in detailsInProduct)
            {
                var detailsInData = _detailDataService.getData(item.DetailId);
                foreach (var item2 in detailsInData)
                {
                    if(isCartIn == false)
                    {
                        if (item2.Price == 0)
                        {
                            ComponentItem itemData = new()
                            {
                                CompName = item2.detail.DetailName,
                                CompValue = item2.DataName,
                                Price = item2.Price,
                                BaseId = item2.Id,
                                ProductId = product.Id,
                                CompId = item2.DetailId,
                            };
                            itemsPriceFree.Add(itemData);
                        }
                    }
                }
                datas.AddRange(detailsInData);
            }

            #region Cookie Have

            if (cookieCart != null)
            {            
                List<ComponentItem> component = new();

                if (cart != null)
                {
                    foreach (var item in cart)
                    {
                        if(item._compItems != null)
                        {
                            component.AddRange(item._compItems);
                        }
                    }

                    var cookieProduct = cart.Where(x => x.Id == product.Id).Select(x => new ProductDto
                    {
                        Price = x.BasePrice,
                        Id = x.Id,

                    }).SingleOrDefault();

                    if (cookieProduct != null)
                    {
                        if(component.Count > 0)
                        {
                            var componentList = component.Where(x => x.CompId == cookieProduct.Id).Select(x => new DetailDataDto
                            {
                                DataName = x.CompName,
                                Price = x.Price,
                                DetailId = x.CompId,

                            }).ToList();

                            ViewBag.DetailsCookie = componentList;
                            ViewBag.JsonCookieListDetails = JsonSerializer.Serialize(component.Where(x => x.ProductId == cookieProduct.Id));
                        }
                       
                        ViewBag.Item = cookieProduct;
                      
                    }
                }

                #endregion

            }

            ViewBag.DetailDataSelectList = new SelectList(datas.Where(x => x.detail.Type == "Dropdown"), "Id", "DataName");
            ViewBag.DetailData = datas;
            ViewBag.ItemDataFreeList = JsonSerializer.Serialize(itemsPriceFree);

        }

        public JsonResult sepettencikar(int Id)
        {
            var optionsAccess = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };

            List<CartItem> result = new();

            var cookie = Request.Cookies["CartItems"];

            if (cookie != null)
            {
                List<CartItem> carts = JsonSerializer.Deserialize<List<CartItem>>(cookie, optionsAccess);
                result.AddRange(carts);

                if (result != null)
                {
                    for (int i = 0; i < result.Count; i++)
                    {
                        if (result[i].Id == Id)
                        {
                            result.RemoveAt(i);
                        }
                    }
                }

                if (result.Count == 0)
                {
                    Response.Cookies.Delete("CartItems");
                }
                else
                {
                    string cartList = JsonSerializer.Serialize(result);
                    Response.Cookies.Append("CartItems", cartList);
                }
            }
            else
            {
                CookieOptions cookieNew = new();
                cookieNew.Expires = DateTime.Now.AddYears(1);
                string componentList = JsonSerializer.Serialize(result);
                Response.Cookies.Append("CartItems", componentList, cookieNew);
            }

            return Json(true);
        }

        public IActionResult sepetcikar(int Id)
        {
            var optionsAccess = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };

            List<CartItem> result = new();

            var cookie = Request.Cookies["CartItems"];

            if (cookie != null)
            {
                List<CartItem> carts = JsonSerializer.Deserialize<List<CartItem>>(cookie, optionsAccess);
                result.AddRange(carts);

                if (result != null)
                {
                    for (int i = 0; i < result.Count; i++)
                    {
                        if (result[i].Id == Id)
                        {
                            result.RemoveAt(i);
                        }
                    }
                }

                if (result.Count == 0)
                {
                    Response.Cookies.Delete("CartItems");
                }
                else
                {
                    string cartList = JsonSerializer.Serialize(result);
                    Response.Cookies.Append("CartItems", cartList);
                }
            }
            else
            {
                CookieOptions cookieNew = new CookieOptions();
                cookieNew.Expires = DateTime.Now.AddYears(1);
                string componentList = JsonSerializer.Serialize(result);
                Response.Cookies.Append("CartItems", componentList, cookieNew);
            }

            return RedirectToAction("sepet", "hizmet");
        }

        [HttpPost]
        public JsonResult sepetitemizle()
        {
            Response.Cookies.Delete("CartItems");
            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> fiyatformgonder(CustomerBook contact)
        {
            string result = "";
            string items = "";
            string li = "";
            int count = 0;
            try
            {
                decimal total = 0;

                var cookieIsHave = Request.Cookies["CartItems"];
                List<CartItem> resultList = JsonSerializer.Deserialize<List<CartItem>>(cookieIsHave);

                foreach (var item in resultList)
                {
                    count += 1;
                    li = "";
                    foreach (var item2 in item._compItems.Where(x=> x.CompId == item.Id))
                    {
                        li += $"<li>{item2.CompName}: {item2.CompValue}</li>";
                    }

                    total += item.BasePrice;
                    items += $"<tr><td style='border: 1px solid black; border-collapse: collapse; padding: 15px;'>{item.Item}<ul>{li}</ul></td><td style='border: 1px solid black; border-collapse: collapse; padding: 5px;'>{Convert.ToInt32(item.BasePrice)} TL</td></tr>";
                }

                CustomerBook contactMessage = new();
                contactMessage.Email = contact.Email;
                contactMessage.Subject = contact.Name + " teklifmatik üzerinden sipariş oluşturdu";
                contactMessage.Phone = contact.Phone;
                contactMessage.Name = contact.Name;
                contactMessage.Surname = contact.Surname;
                contactMessage.Content = contact.Content;
                contactMessage.To = "dogancanaras49@gmail.com";
                contactMessage.Total = Convert.ToInt32(total);
                contactMessage.cartResult = resultList;
                contactMessage.Content = $@"<p>{contactMessage.Name} {contactMessage.Surname} iletişim formunu doldurdu. (Bu form https://reklamlutfen.com üzerinden gelmiştir.) <p><strong>Telefon: </strong>{contactMessage.Phone}</p> <hr/> <p><strong>Email Adresi:</strong> {contactMessage.Email}</p> <hr/> <p><strong>Toplam Teklif:</strong> {contactMessage.Total} TL</p> <hr/> <p><strong style='text-align:left;'>Hizmetler:</strong></p> <table style='table-layout: auto; width: 330px;'><thead><tr><th style='text-align:center; border: 1px solid black; border-collapse: collapse; padding:5px;'>Hizmet Adı</th><th style='text-align:left; border: 1px solid black; padding:5px; border-collapse: collapse;'>Fiyat</th></tr></thead><tbody>{items}</tbody></table>";

                //contactMessage.Content = $@"<p>{contactMessage.NameSurname} iletişim formunu doldurdu. (Bu form https://ikifikir.net/fiyatpaketleri üzerinden gelmiştir.) <p> <hr/> <p><strong>Email Adresi:</strong> {contactMessage.EmailAdress}</p> <hr/> <p>{contactMessage.Message}</p> <hr/> <p><strong>Telefon: </strong>{contactMessage.PhoneNumber}</p> <hr/> <p><strong>Toplam Teklif:</strong> {contactMessage.Total}TL</p> <hr/> <p><strong>Hizmetler:</strong></p> <table style='table-layout: auto; width: 215px;'><thead><tr><th style='text-align:center; border: 1px solid black; border-collapse: collapse; padding:5px;'>Hizmet Adı</th><th style='text-align:left; border: 1px solid black; padding:5px; border-collapse: collapse;'>Fiyat</th></tr></thead><tbody>{items}</tbody></table>";

                result = await _emailSender.SendEmailAsync(contactMessage);
                Random code = new();
                string numberCode = code.Next(50000, 70000).ToString();

                CustomerDto cust = new()
                {
                    EmailAddress = contactMessage.Email,
                    Messagess = contactMessage.Content,
                    NameSurname = contactMessage.Name +" "+ contactMessage.Surname,
                    PhoneNumber = contactMessage.Phone,
                    IsActive = true
                };

                int resultCustomer = _customerService.InsertCustomer(cust);

                OrdersDto newOrder = new()
                {
                    StatusId = 2,
                    Quantity = count,
                    TotalPrice = resultList.Sum(x => x.BasePrice),
                    IsActive = true,
                    OrderNo = numberCode,
                    CustomerId = resultCustomer,
                    OrderDetailHtml = contactMessage.Content
                };

                _orderService.Insert(newOrder);

                Response.Cookies.Delete("CartItems");
                return RedirectToAction("sonuc", "anasayfa");

            }
            catch (Exception)
            {
                Response.Cookies.Delete("CartItems");
                return RedirectToAction("sonuc", "anasayfa");

            }

            #endregion
        }

        private void Meta(string title)
        {
            MetaViewModel meta = new();
            meta.Image = "https://reklamlutfen.com/img/logo.png";
            meta.Title = title;
            meta.ogDescription = "Reklam Lütfen ile markanıza uygun hizmetleri inceleyip teklif oluşturun! Markanızın neye ihtiyacı varsa bütün işleri bizde! Hemen teklif oluşturun!";
            meta.Description = "Reklam Lütfen ile markanıza uygun hizmetleri inceleyip teklif oluşturun! Markanızın neye ihtiyacı varsa bütün işleri bizde! Hemen teklif oluşturun!";
            meta.ogTitle = title;
            meta.Url = "https://reklamlutfen.com";
            ViewBag.Meta = meta;
        }
    }
}

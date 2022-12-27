using ajansflix.COMMON.Helpers;
using ajansflix.COMMON.Helpers.Cyroptography;
using ajansflix.Models;
using ajansflix.SERVICES.Dtos.CustomerData;
using ajansflix.SERVICES.Dtos.ProductsData;
using ajansflix.SERVICES.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ajansflix.Controllers
{
    public class anasayfaController : Controller
    {

        #region Fields

        private ICategoryService _categoryService;
        private ICustomerService _customerService;
        private IProductService _productService;
        public anasayfaController(ICategoryService categoryService, ICustomerService customerService, IProductService productService)
        {
            _categoryService = categoryService;
            _customerService = customerService;
            _productService = productService;
        }

        #endregion

        #region Pages

        public IActionResult sayfa(int? pageNumber, string keywords)
        {

            Meta("Reklam Lütfen");
            BindDatabaseData(pageNumber);

            var allProducts = _productService.GetAll();
            ViewBag.ProductForMenu = allProducts;

            return View();
        }

        private void Meta(string title)
        {
            MetaViewModel meta = new MetaViewModel();
            meta.Image = "https://reklamlutfen.com/img/logo.png";
            meta.Title = title;
            meta.ogDescription = "Reklam Lütfen ile markanıza uygun hizmetleri inceleyip teklif oluşturun! Markanızın neye ihtiyacı varsa bütün işleri bizde! Hemen teklif oluşturun!";
            meta.Description = "Reklam Lütfen ile markanıza uygun hizmetleri inceleyip teklif oluşturun! Markanızın neye ihtiyacı varsa bütün işleri bizde! Hemen teklif oluşturun!";
            meta.ogTitle = title;
            meta.Url = "https://reklamlutfen.com";
            ViewBag.Meta = meta;
        }

        [Route("girisyap")]
        public IActionResult girisyap()
        {

            #region Kayıt

            if (Convert.ToInt32(ViewData["basari"]) == -1 || Convert.ToInt32(ViewData["basari"]) == 2 || Convert.ToInt32(ViewData["basari"]) == 1)
            {
                ViewData["display"] = "block";
            }
            else
            {
                ViewData["display"] = "none";
            }

            #endregion

            #region Oturum Aç

            if (Convert.ToInt32(ViewData["basari2"]) == -1 || Convert.ToInt32(ViewData["basari2"]) == 1 || Convert.ToInt32(ViewData["basari2"]) == 2)
            {
                ViewData["display2"] = "block";
            }
            else
            {
                ViewData["display2"] = "none";
            }

            #endregion

            return View();
        }

        [Route("sonuc")]
        public IActionResult sonuc()
        {
            Meta("Reklam Lütfen | Sonuç");
            return View();
        }

        [Route("iletisim")]
        public IActionResult iletisim()
        {
            Meta("Reklam Lütfen | İletişim");
            return View();
        }

        [Route("musteri")]
        public IActionResult musteri()
        {
            Meta("Reklam Lütfen | Müşteri");
            return View();
        }

        [Route("yardim")]
        public IActionResult yardim()
        {
            Meta("Reklam Lütfen | Yardım");
            return View();
        }

        [Route("/anasayfa/hata/{code:int}")]
        public IActionResult hata(int code)
        {
            Meta("Reklam Lütfen | 404");
            ViewData["ErrorCode"] = $"{code}";
            return View("~/Views/anasayfa/hata.cshtml");
        }

        #endregion

        #region Extern

        public void BindDatabaseData(int? pageNumber)
        {
            var categoryList = _categoryService.listCategoryByWeb();
            ViewBag.CategoryList = categoryList;

            var products = _productService.listProductToWeb();
            ViewBag.Products = products;

        }

        //[HttpPost]
        //public IActionResult oturumac(CustomerDto model)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            string passCrypto = new Crypto().TryEncrypt(model.Password);
        //            bool result = _customerService.logIn(model.UserName, passCrypto);

        //            if(result != false)
        //            {
        //                var getCustomer = _customerService.getCustomer(model.UserName);

        //                var userClaims = new List<Claim>()
        //                {
        //                    new Claim("Name",getCustomer.NameSurname),
        //                    new Claim("UserEmail",getCustomer.EmailAddress),
        //                    new Claim("UserId", getCustomer.Id.ToString()),
        //                    new Claim(ClaimTypes.Name,getCustomer.NameSurname),
        //                    new Claim(ClaimTypes.NameIdentifier, getCustomer.Id.ToString()),
        //                };

        //                var userIdentity = new ClaimsIdentity(userClaims, "User Identity");
        //                var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });

        //                HttpContext.SignInAsync(
        //                    userPrincipal,
        //                    new AuthenticationProperties
        //                    {
        //                        IsPersistent = true,
        //                        ExpiresUtc = DateTime.UtcNow.AddYears(15)
        //                    });
                        
        //                return Redirect("/anasayfa");
        //            }
        //            else
        //            {
        //                ViewData["sonuc2"] = "Kullanıcı adı veya şifre yanlış!";
        //                ViewData["basari2"] = 2;
        //                ViewData["display2"] = "block";
        //                ViewData["baslangic2"] = 1;
        //                return View(nameof(girisyap));
        //            }
        //        }
        //        else
        //        {
        //            ViewData["sonuc2"] = "Gerekli alanlar doldurulmadı!";
        //            ViewData["basari2"] = -1;
        //            ViewData["display2"] = "block";
        //            ViewData["baslangic"] = 1;
        //            return View(nameof(girisyap));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewData["sonuc2"] = "Sistemden kaynaklı bir sorun meydana geldi!";
        //        ViewData["basari2"] = 1;
        //        ViewData["display2"] = "block";
        //        ViewData["baslangic"] = 1;

        //        return View(nameof(girisyap));

        //    }
        //}

        //[HttpPost]
        //public IActionResult kayitol(CustomerDto model)
        //{

        //    try
        //    {
        //        if(model.Password == model.RePassword)
        //        {
        //            string crypto = new Crypto().TryEncrypt(model.Password);
        //            model.Password = crypto;
        //            _customerService.Insert(model);
              
        //            ViewData["sonuc"] = "Üyelik İşlemi başarıyla tamamlandı!";
        //            ViewData["basari"] = 1;
        //            ViewData["display"] = "block";
        //            ViewData["baslangic"] = 1;
        //        }
        //        else
        //        {
        //            ViewData["sonuc"] = "Şifreler uyuşmuyor!";
        //            ViewData["basari"] = 2;
        //            ViewData["display"] = "block";
        //            ViewData["baslangic"] = 1;
        //        }

        //        return View(nameof(girisyap));
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewData["sonuc"] = "Sistemden kaynaklı bir sorun meydana geldi!";
        //        ViewData["basari"] = -1;
        //        ViewData["display"] = "block";
        //        ViewData["baslangic"] = 1;

        //        return View(nameof(girisyap));
        //    }
        //}

        public IActionResult cikisyap()
        {
            HttpContext.SignOutAsync();
            return Redirect("/anasayfa");
        }

        #endregion

        #region Mobile

        public IActionResult _mobileView()
        {
            return PartialView("_mobileView");
        }

        #endregion

        #region Desktop

        public IActionResult _desktopView()
        {
            return PartialView("_desktopView");
        }

        #endregion

    }
}

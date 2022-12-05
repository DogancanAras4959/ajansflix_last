using ajansflix.COMMON.Helpers.Cyroptography;
using ajansflix.SERVICES.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ajansflix.Areas.admin.Models;
using ajansflix.SERVICES.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ajansflix.SERVICES.Dtos.UserData;
using ajansflix.Areas.admin.Data;

namespace ajansflix.Areas.admin.Controllers
{
    [Area("admin")]
    public class editorController : Controller
    {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
     
        public editorController(IUserService userService, IOrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }

        [CheckLogin]
        public IActionResult home()
        {
            ViewBag.Orders = _orderService.listOrderWithCustomer();
            ViewBag.OrderTotal = _orderService.TotalCount();
            ViewBag.OrderTotalPaid = _orderService.TotalCountPaid();
            ViewBag.OrderTotalUnPaid = _orderService.TotalCountUnpaid();
            ViewBag.OrderTotalCancel = _orderService.TotalCountCancel();
            return View();
        }

        [HttpGet]
        public IActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult loginToEditor(LoginViewModel model)
        {
            try
            {
                string passCrypto = new Crypto().TryEncrypt(model.Password);
                var user = _userService.login(model.UserName, passCrypto);

                if (user == false)
                {
                    ViewBag.Message = "Geçersiz Kimlik";
                    return Redirect("/admin/editor/login");
                }
                else
                {

                    var getUser = _userService.getUserByName(model.UserName);

                    var settings = new Newtonsoft.Json.JsonSerializerSettings();
                    settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                    string data = JsonConvert.SerializeObject(getUser, settings);
                    var deSerilizeData = JsonConvert.DeserializeObject(data).ToString();
                    var resultConvert = JsonConvert.DeserializeObject<UserDto>(deSerilizeData);
                    SessionExtensionMethod.SetObject(HttpContext.Session, "account", resultConvert);

                    return Redirect("/admin/editor/home");
                }
            }
            catch (Exception)
            {
                return Redirect("/admin/editor/login");
            }
        }

        public IActionResult logout()
        {
            HttpContext.Session.Remove("account");
            HttpContext.Session.Clear();
            return Redirect("/admin/editor/login");
        }

    }
}

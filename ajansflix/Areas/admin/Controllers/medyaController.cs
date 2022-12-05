using ajansflix.Areas.admin.Data;
using ajansflix.SERVICES.Dtos.ImageData;
using ajansflix.SERVICES.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ajansflix.Areas.admin.Controllers
{
    [Area("admin")]
    public class medyaController : Controller
    {
        private readonly IImageService _imageService;
        private readonly IVideoDataService _videoService;

        public medyaController(IImageService imageService, IVideoDataService videoDataService) 
        {
            _imageService = imageService;
            _videoService = videoDataService;
        }

        [HttpGet]
        [CheckLogin]
        public async Task<IActionResult> fotograflar() => View(await _imageService.GetAllImages());

        [HttpPost]
        public async Task<IActionResult> upload(IFormFile[] file)
        {
            if (file.Length > 10)
            {
                this.ModelState.AddModelError("images", "You cannot upload more than 10 images!");
                return View();
            }

            await _imageService.Process(file.Select(i => new ImageInputModel
            {
                Name = i.FileName,
                Type = i.ContentType,
                Content = i.OpenReadStream()
            }));

            return Redirect("/admin/medya/fotograflar");

        }

        [HttpPost]
        public async Task<IActionResult> uploadWithProduct(IFormFile[] file, int ProductId)
        {
            if(file.Length > 10)
            {
                this.ModelState.AddModelError("images", "You cannot upload more than 10 images!");
                return View();
            }

            await _imageService.ProcessProductImages(file.Select(i => new ImageInputModel
            {
                Name = i.FileName,
                Type = i.ContentType,
                Content = i.OpenReadStream()
            }), ProductId);

            return Redirect("/admin/urun/urunduzenle?id="+ProductId);
        }

        public async Task<IActionResult> thumbnail(string id)
        {
            var headers = Response.GetTypedHeaders();
            headers.CacheControl = new CacheControlHeaderValue
            {
                Public = true,
                MaxAge = TimeSpan.FromDays(30),
            };

            headers.Expires = new DateTimeOffset(DateTime.UtcNow.AddDays(30));

            return File(await _imageService.GetThumbnail(id), "image/jpg");
        }

        public async Task<IActionResult> fullScreen(string id)
        {
            var headers = Response.GetTypedHeaders();
            headers.CacheControl = new CacheControlHeaderValue
            {
                Public = true,
                MaxAge = TimeSpan.FromDays(30),
            };

            headers.Expires = new DateTimeOffset(DateTime.UtcNow.AddDays(30));

            return File(await _imageService.GetFullScreen(id), "image/jpg");
        }

    }
}

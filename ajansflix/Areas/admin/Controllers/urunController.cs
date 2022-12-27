using ajansflix.Areas.admin.Data;
using ajansflix.COMMON.Helpers;
using ajansflix.SERVICES.Dtos.CategoryData;
using ajansflix.SERVICES.Dtos.DetailData;
using ajansflix.SERVICES.Dtos.DetailDataData;
using ajansflix.SERVICES.Dtos.ImageData;
using ajansflix.SERVICES.Dtos.ProductDetailsData;
using ajansflix.SERVICES.Dtos.ProductsData;
using ajansflix.SERVICES.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.Areas.admin.Controllers
{
    [Area("admin")]
    public class urunController : Controller
    {

        #region Fields

        private readonly IProductService _productService;
        private readonly IDetailService _detailService;
        private readonly IDetailDataService _detailDataService;
        private readonly IProductDetailService _productDetailService;
        private readonly ICategoryService _categoryService;
        private readonly IImageService _imageService;

        [Obsolete]
        private IHostingEnvironment _hostingEnvironment;

        [Obsolete]
        public urunController(IHostingEnvironment hostingEnvironment, IProductService productService, IDetailService detailService, IDetailDataService detailDataService, IProductDetailService productDetailService, ICategoryService categoryService, IImageService imageService)
        {
            _productService = productService;
            _detailDataService = detailDataService;
            _detailService = detailService;
            _productDetailService = productDetailService;
            _categoryService = categoryService;
            _imageService = imageService;
            _productDetailService = productDetailService;
            _hostingEnvironment = hostingEnvironment;
        }

        #endregion

        #region Kategoriler

        [CheckLogin]
        public IActionResult kategoriler()
        {
            BindCategories();
            return View();
        }

        [CheckLogin]
        public async Task<IActionResult> kategoriekle()
        {
            ViewBag.Images = await _imageService.GetAllImages();
            return View();
        }

        [CheckLogin]
        public async Task<IActionResult> kategoriduzenle(int Id)
        {
            ViewBag.Images = await _imageService.GetAllImages();
            var category = _categoryService.Get(Id);
            return View(category);
        }

        [HttpPost]
        [RequestSizeLimit(10 * 1024 * 1024)] //En fazla 10mb kadar dosya yüklenebilir
        public async Task<IActionResult> kategoriguncelle(CategoryDto model, IFormFile file)
        {
            var category = _categoryService.Get(model.Id);

            if (file != null)
            {

                string path = await _imageService.ProcessFile(new ImageInputModel
                {
                    Name = file.FileName,
                    Type = file.ContentType,
                    Content = file.OpenReadStream()
                });

                model.CategoryImage = path;

            }
            else
            {
                model.CategoryImage = category.CategoryImage;
            }

            category.CategoryName = model.CategoryName;
            category.CategoryDescription = model.CategoryDescription;
            category.CategoryImage = model.CategoryImage;
            category.CategoryStatus = model.CategoryStatus;
            category.CategorySorted = model.CategorySorted;
            category.StatusCode = model.StatusCode;
            category.UpdatedTime = DateTime.Now;
            category.IsActive = model.IsActive;

            _categoryService.Update(category);

            return Redirect("/admin/urun/kategoriduzenle?Id=" + category.Id);
        }

        public IActionResult kategoridurumduzenle(int Id)
        {
            var category = _categoryService.Get(Id);
            if (category.IsActive == true)
            {
                category.IsActive = false;
            }
            else
            {
                category.IsActive = true;
            }

            _categoryService.Update(category);
            return Redirect("/admin/urun/kategoriler");
        }

        public IActionResult kategorisil(int Id)
        {
            var category = _categoryService.Get(Id);
            _categoryService.Delete(category.Id);

            return Redirect("/admin/urun/kategoriler");
        }

        [HttpPost]
        [RequestSizeLimit(10 * 1024 * 1024)] //En fazla 10mb kadar dosya yüklenebilir
        public async Task<IActionResult> kategoriolustur(CategoryDto model, IFormFile file)
        {
            if (file != null)
            {
                string path = await _imageService.ProcessFile(new ImageInputModel
                {
                    Name = file.FileName,
                    Type = file.ContentType,
                    Content = file.OpenReadStream()
                });

                model.CategoryImage = path;

                _categoryService.Insert(model);
            }
            else
            {
                TempData["status"] = 1;
                TempData["Warning"] = "Fotoğraf yüklemeden geçilemez";
                return Redirect("/admin/urun/kategoriekle");
            }
            return Redirect("/admin/urun/kategoriler");
        }

        #endregion

        #region Urunler

        [CheckLogin]
        public IActionResult urunler(int? pageNumber)
        {
            int pageSize = 100;

            var products = _productService.listProductToAdmin();
            ViewBag.Products = PaginationList<ProductDto>.Create(products, pageNumber ?? 1, pageSize);
            return View();
        }

        [CheckLogin]
        public async Task<IActionResult> urunekle()
        {
            var categories = _categoryService.GetAll();
            ViewBag.Category = new SelectList(categories, "Id", "CategoryName");
            ViewBag.Images = await _imageService.GetAllImages();

            return View();
        }

        [CheckLogin]
        public async Task<IActionResult> urunduzenle(int Id)
        {
            var product = _productService.getProduct(Id);
            var categories = _categoryService.GetAll();
            ViewBag.Category = new SelectList(categories, "Id", "CategoryName", product.CategoryId);
            ViewBag.Images = await _imageService.GetAllImages();

            var bilesenler = _detailService.GetAll();
            var productDetail = _productDetailService.listDetails(product.Id);

            ViewBag.Bilesenler = bilesenler;
            ViewBag.BilesenUrun = productDetail;
            ViewBag.ImagesProduct = await _imageService.GetAllImagesByProductId(product.Id);

            return View(product);
        }

        public IActionResult deleteImage(string path, int productId)
        {
            int result = _imageService.GetImageFromProduct(path, productId);
            return Redirect("/admin/urun/urunduzenle?Id=" + productId);
        }

        [CheckLogin]
        public IActionResult urundurumduzenle(int Id)
        {
            var product = _productService.getProduct(Id);
            if (product.IsActive == true)
            {
                product.IsActive = false;
            }
            else
            {
                product.IsActive = true;
            }

            _productService.Update(product);

            return Redirect("/admin/urun/urunler");
        }

        [CheckLogin]
        public IActionResult urunsil(int Id)
        {
            var product = _productService.getProduct(Id);
            _productService.Delete(product.Id);
            return Redirect("/admin/urun/urunler");
        }

        [HttpPost]
        public async Task<IActionResult> urunguncelle(ProductDto model, IFormFile file)
        {
            try
            {
                var product = _productService.getProduct(model.Id);

                if (file != null)
                {
                    string path = await _imageService.ProcessFile(new ImageInputModel
                    {
                        Name = file.FileName,
                        Type = file.ContentType,
                        Content = file.OpenReadStream()
                    });
                    product.ImagePath = path;
                }

                product.Price = Convert.ToInt32(model.Price);
                product.ProductMetaName = model.ProductMetaName;
                product.ProductMetaDescription = model.ProductMetaDescription;
                product.CategoryId = model.CategoryId;
                product.ProductDescription = model.ProductDescription;
                product.Discount = model.Discount;
                product.IsActive = model.IsActive;
                product.ProductAlternateDesc = model.ProductAlternateDesc;
                product.Banner = model.Banner;
                product.ProductName = model.ProductName;
                _productService.Update(product);

                return Redirect("/admin/urun/urunduzenle?Id=" + product.Id);
            }
            catch (Exception)
            {
                return Redirect("/admin/urun/urunler");
            }
        }

        [HttpPost]
        public async Task<IActionResult> urunolustur(ProductDto model, IFormFile file)
        {
            if (file != null)
            {
                string path = await _imageService.ProcessFile(new ImageInputModel
                {
                    Name = file.FileName,
                    Type = file.ContentType,
                    Content = file.OpenReadStream()
                });

                model.ImagePath = path;
            }
            else
            {
                TempData["status"] = 1;
                TempData["Warning"] = "Fotoğraf yüklemeden geçilemez";
                return Redirect("/admin/urun/urunekle");
            }

            model.Price = Convert.ToInt32(model.Price);
            _productService.Insert(model);

            return Redirect("/admin/urun/urunler");
        }

        [HttpGet]
        [CheckLogin]
        public IActionResult disaaktar()
        {
            var productList = _productService.listProductToAdmin();
            var productDetailList = _productDetailService.listToAdmin();
            foreach (var item in productList)
            {
                foreach (var item2 in productDetailList.Where(x => x.ProductId == item.Id))
                {
                    item.productDetails.Add(item2);
                }
            }
            return View(productList);
        }

        #endregion

        #region Bileşenler

        [CheckLogin]
        public IActionResult urunbilesenleri()
        {
            BindDetails();
            return View();
        }

        [CheckLogin]
        public IActionResult bilesenduzenle(int Id)
        {
            var bilesen = _detailService.Get(Id);
            BindDetails();
            return View(bilesen);
        }

        public IActionResult bilesensil(int Id)
        {
            var bilesen = _detailService.Get(Id);
            _detailService.Delete(bilesen.Id);
            return Redirect("/admin/urun/urunbilesenleri");

        }
        public IActionResult bilesendurumduzenle(int Id)
        {
            var bilesen = _detailService.Get(Id);
            if (bilesen.IsActive == true)
            {
                bilesen.IsActive = false;
            }
            else
            {
                bilesen.IsActive = true;
            }

            _detailService.Update(bilesen);

            return Redirect("/admin/urun/urunbilesenleri");

        }

        [HttpPost]
        public IActionResult bilesenolustur(DetailDto model)
        {
            _detailService.Insert(model);
            return Redirect("/admin/urun/urunbilesenleri");

        }
        public IActionResult bilesenguncelle(DetailDto model, int Id)
        {
            var bilesen = _detailService.Get(Id);

            bilesen.DetailName = model.DetailName;
            bilesen.Type = model.Type;
            bilesen.UpdatedTime = DateTime.Now;
            bilesen.IsActive = model.IsActive;
            _detailService.Update(bilesen);

            return Redirect("/admin/urun/urunbilesenleri");

        }

        #endregion

        #region Bileşen Varyantları

        [CheckLogin]
        public IActionResult varyantekle(int Id)
        {
            var bilesen = _detailService.Get(Id);
            ViewBag.Bilesen = bilesen;
            ViewBag.Varyants = _detailDataService.getDataAdmin(bilesen.Id);

            var settings = new Newtonsoft.Json.JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            string data = JsonConvert.SerializeObject(bilesen, settings);
            var deSerilizeData = JsonConvert.DeserializeObject(data).ToString();
            var resultConvert = JsonConvert.DeserializeObject<DetailDto>(deSerilizeData);
            SessionExtensionMethod.SetObject(HttpContext.Session, "component", resultConvert);


            return View();
        }

        [CheckLogin]
        public IActionResult varyantduzenle(int Id)
        {
            var varyant = _detailDataService.Get(Id);
            var bilesen = _detailService.Get(varyant.DetailId);

            ViewBag.Bilesen = bilesen;
            ViewBag.Varyants = _detailDataService.getDataAdmin(bilesen.Id);

            return View(varyant);
        }

        [HttpPost]
        public async Task<IActionResult> varyantolustur(DetailDataDto model, IFormFile file)
        {
            DetailDto component = SessionExtensionMethod.GetObject<DetailDto>(HttpContext.Session, "component");
            int detailId = component.Id;
            if (file != null)
            {
                string path = await _imageService.ProcessFile(new ImageInputModel
                {

                    Name = file.FileName,
                    Type = file.ContentType,
                    Content = file.OpenReadStream()
                });

                model.ImagePath = path;
            }

            model.DetailId = component.Id;
            _detailDataService.Insert(model);

            HttpContext.Session.Remove("component");
            return Redirect("/admin/urun/varyantekle?Id=" + detailId);

        }

        [HttpPost]
        public async Task<IActionResult> varyantguncelle(DetailDataDto model, IFormFile file)
        {
            var varyant = _detailDataService.Get(model.Id);

            if (file != null)
            {
                string path = await _imageService.ProcessFile(new ImageInputModel
                {
                    Name = file.FileName,
                    Type = file.ContentType,
                    Content = file.OpenReadStream(),
                });

                model.ImagePath = path;
            }
            else
            {
                model.ImagePath = varyant.ImagePath;
            }

            varyant.Price = model.Price;
            varyant.IsActive = model.IsActive;
            varyant.IsLock = model.IsLock;
            varyant.DataName = model.DataName;
            varyant.UpdatedTime = DateTime.Now;
            varyant.ImagePath = model.ImagePath;

            _detailDataService.Update(varyant);

            return Redirect("/admin/urun/varyantekle?id=" + varyant.DetailId);
        }

        public IActionResult varyantsil(int Id)
        {
            var varyant = _detailDataService.Get(Id);
            _detailDataService.Delete(varyant.Id);

            int bilesenId = varyant.DetailId;

            return Redirect("/admin/urun/varyantekle?Id=" + bilesenId);
        }

        public IActionResult varyantdurumduzenle(int Id)
        {
            var varyant = _detailDataService.Get(Id);
            int bilesenId = varyant.DetailId;

            if (varyant.IsActive == true)
            {
                varyant.IsActive = false;
            }
            else
            {
                varyant.IsActive = true;
            }

            _detailDataService.Update(varyant);

            return Redirect("/admin/urun/varyantekle?Id=" + bilesenId);
        }

        #endregion

        #region Extern

        public void BindCategories()
        {
            var categories = _categoryService.GetAll();
            ViewBag.Categories = categories;
        }
        public void BindDetails()
        {
            var details = _detailService.GetAll();
            ViewBag.Details = details;
        }
        [HttpPost]
        public JsonResult mergeProduct(int Id, int ProductId)
        {
            var product = _productService.Get(ProductId);
            var detail = _detailService.Get(Id);

            ProductDetailDto productDetail = new ProductDetailDto
            {
                DetailId = detail.Id,
                ProductId = product.Id,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                IsActive = true,
            };

            _productDetailService.Insert(productDetail);

            return Json(true);
        }

        [HttpPost]
        public JsonResult removeMergeProduct(int Id, int ProductId)
        {
            var productDetail = _productDetailService.getComponent(Id, ProductId);
            _productDetailService.Delete(productDetail.Id);
            return Json(true);
        }

        [Obsolete]
        [HttpPost]
        public IActionResult iceaktar()
        {
            try
            {
                IFormFile file = Request.Form.Files[0];
                string folderName = "files";
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                StringBuilder sb = new StringBuilder();
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string sFileExt = Path.GetExtension(file.FileName).ToLower();
                    ISheet sheet;
                    string fullPath = Path.Combine(newPath, file.FileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        stream.Position = 0;
                        if (sFileExt == ".xls")
                        {
                            HSSFWorkbook hssfwb = new HSSFWorkbook(stream);
                            sheet = hssfwb.GetSheetAt(0);
                        }
                        else
                        {
                            XSSFWorkbook hssfwb = new XSSFWorkbook(stream);
                            sheet = hssfwb.GetSheetAt(0);
                        }
                        IRow headerRow = sheet.GetRow(0);
                        int cellCount = headerRow.LastCellNum;
                        for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
                        {

                            IRow row = sheet.GetRow(i);
                            ProductDto product = new ProductDto();

                            if (row.GetCell(0) != null)
                            {
                                int getId = 0;
                                ProductDto productGet;

                                if (row.GetCell(0).ToString() != "")
                                {
                                    getId = Convert.ToInt32(row.GetCell(0).ToString());
                                    productGet = _productService.getProduct(getId);
                                }
                                else
                                {
                                    productGet = null;
                                }

                                //Update Ürün

                                if (productGet != null)
                                {
                                    productGet.Banner = "";
                                    productGet.UpdatedTime = DateTime.Now;
                                    productGet.ProductName = row.GetCell(1).ToString();

                                    if (row.GetCell(2) != null)
                                    {
                                        var category = _categoryService.categoryByName(row.GetCell(2).ToString());
                                        if (category != null)
                                        {
                                            productGet.CategoryId = category.Id;
                                        }
                                        else
                                        {
                                            CategoryDto categoryNew = new CategoryDto
                                            {
                                                CategoryName = row.GetCell(2).ToString(),
                                                CategoryDescription = "",
                                                CategoryImage = "",
                                                CategoryStatus = "",
                                                StatusCode = "",
                                            };
                                            int resultCategoryId = _categoryService.InsertCategoryById(categoryNew);
                                            if (resultCategoryId > 0)
                                            {
                                                productGet.CategoryId = resultCategoryId;
                                            }
                                        }

                                    }

                                    if (row.GetCell(3) != null)
                                        productGet.Price = Convert.ToInt32(row.GetCell(3).ToString());

                                    if (row.GetCell(4) != null)
                                        productGet.Discount = Convert.ToInt32(row.GetCell(4).ToString());

                                    if (row.GetCell(5) != null)
                                        productGet.ProductAlternateDesc = row.GetCell(5).ToString();

                                    if (row.GetCell(6) != null)
                                        productGet.ProductDescription = row.GetCell(6).ToString();

                                    if (row.GetCell(7) != null)
                                        productGet.ProductMetaName = row.GetCell(7).ToString();

                                    if (row.GetCell(8) != null)
                                        productGet.ProductMetaDescription = row.GetCell(8).ToString();

                                    if (row.GetCell(9) != null)
                                        productGet.ImagePath = row.GetCell(9).ToString();

                                    if (row.GetCell(10) != null)
                                    {
                                        char[] splitIcon = { ',' };
                                        string splitData = row.GetCell(10).ToString();
                                        string[] items = splitData.Split(splitIcon);

                                        foreach (var item in items)
                                        {
                                            var detail = _detailService.detailGet(item);

                                            if (detail == null)
                                            {
                                                DetailDto newDetail = new DetailDto
                                                {
                                                    IsActive = true,
                                                    DetailName = item,
                                                    Type = "List",
                                                };

                                                int resultIdDetail = _detailService.InsertDetailById(newDetail);

                                                if (resultIdDetail > 0)
                                                {
                                                    ProductDetailDto newProductDetailDto = new ProductDetailDto
                                                    {
                                                        ProductId = productGet.Id,
                                                        DetailId = resultIdDetail,
                                                        IsActive = true,
                                                    };
                                                    _productDetailService.Insert(newProductDetailDto);
                                                }
                                            }
                                            else
                                            {
                                                var detailProduct = _productDetailService.getComponent(detail.Id, getId);
                                                if (detailProduct == null)
                                                {

                                                    ProductDetailDto newProductDetailDto = new ProductDetailDto
                                                    {
                                                        ProductId = productGet.Id,
                                                        DetailId = detail.Id,
                                                        IsActive = true,
                                                    };
                                                    _productDetailService.Insert(newProductDetailDto);
                                                }
                                            }
                                        }
                                    }
                                    if (row.GetCell(11).ToString() != null)
                                    {
                                        if (row.GetCell(11).ToString() == "Aktif")
                                        {
                                            productGet.IsActive = true;
                                        }
                                        else if (row.GetCell(11).ToString() == "Pasif")
                                        {
                                            productGet.IsActive = false;
                                        }
                                    }
                                    _productService.Update(productGet);
                                }

                                // Insert Urun

                                else
                                {
                                    if (row.GetCell(1) != null)
                                        product.ProductName = row.GetCell(1).ToString();

                                    if (row.GetCell(2) != null)
                                    {
                                        var category = _categoryService.categoryByName(row.GetCell(2).ToString());
                                        if (category != null)
                                        {
                                            product.CategoryId = category.Id;
                                        }
                                        else
                                        {
                                            CategoryDto categoryNew = new CategoryDto
                                            {
                                                CategoryName = row.GetCell(2).ToString(),
                                                CategoryDescription = "",
                                                CategoryImage = "",
                                                CategoryStatus = "",
                                                StatusCode = "",
                                            };
                                            int resultCategoryId = _categoryService.InsertCategoryById(categoryNew);
                                            if (resultCategoryId > 0)
                                            {
                                                product.CategoryId = resultCategoryId;
                                            }
                                        }
                                    }
                                    if (row.GetCell(3) != null)
                                        product.Price = Convert.ToDecimal(row.GetCell(3).ToString());

                                    if (row.GetCell(4) != null)
                                        product.Discount = Convert.ToInt32(row.GetCell(4).ToString());

                                    if (row.GetCell(5) != null)
                                        product.ProductAlternateDesc = row.GetCell(5).ToString();

                                    if (row.GetCell(6) != null)
                                        product.ProductDescription = row.GetCell(6).ToString();

                                    if (row.GetCell(7) != null)
                                        product.ProductMetaName = row.GetCell(7).ToString();

                                    if (row.GetCell(8) != null)
                                        product.ProductMetaDescription = row.GetCell(8).ToString();

                                    if (row.GetCell(9) != null)
                                        product.ImagePath = row.GetCell(9).ToString();

                                    int resultIdProduct = _productService.InsertProductById(product);

                                    if (row.GetCell(10) != null)
                                    {
                                        char[] splitIcon = { ',' };
                                        string splitData = row.GetCell(10).ToString();
                                        string[] items = splitData.Split(splitIcon);

                                        foreach (var item in items)
                                        {
                                            var detail = _detailService.detailGet(item);

                                            if (detail == null)
                                            {
                                                DetailDto newDetail = new DetailDto
                                                {
                                                    IsActive = true,
                                                    DetailName = item,
                                                    Type = "List",
                                                };

                                                int resultIdDetail = _detailService.InsertDetailById(newDetail);

                                                if (resultIdProduct > 0 && resultIdDetail > 0)
                                                {
                                                    ProductDetailDto newProductDetailDto = new ProductDetailDto
                                                    {
                                                        ProductId = resultIdProduct,
                                                        DetailId = resultIdDetail,
                                                        IsActive = true,
                                                    };
                                                    _productDetailService.Insert(newProductDetailDto);
                                                }
                                            }
                                            else
                                            {
                                                ProductDetailDto newProductDetailDto = new ProductDetailDto
                                                {
                                                    ProductId = resultIdProduct,
                                                    DetailId = detail.Id,
                                                    IsActive = true,
                                                };
                                                _productDetailService.Insert(newProductDetailDto);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return Redirect("/admin/urun/urunler");
            }
            
            catch(Exception ex)
            { 
                return Redirect("/admin/urun/urunler"); 
            }
               
           
        }

        [HttpPost]
        public JsonResult siradegistir(int urunId, int detailId, int sortNumber)
        {
            var urun = _productService.getProduct(urunId);
            var detail = _detailService.Get(detailId);

            var productDetail = _productDetailService.getComponent(detail.Id, urun.Id);
            productDetail.Sorted = sortNumber;
            _productDetailService.Update(productDetail);

            return Json(true);

        }

        #endregion

    }
}

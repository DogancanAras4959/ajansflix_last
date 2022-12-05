using ajansflix.SERVICES.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ajansflix.Components
{
    public class SubHeaderViewComponent : ViewComponent
    {
        private ICategoryService _categoryService;

        public SubHeaderViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            var categoryList = _categoryService.GetAll();
            ViewBag.CategoryList = categoryList;
            return View();
        }

        
    }
}

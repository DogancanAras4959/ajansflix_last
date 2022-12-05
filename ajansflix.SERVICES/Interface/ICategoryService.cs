using ajansflix.DOMAIN.Models;
using ajansflix.SERVICES.Crud;
using ajansflix.SERVICES.Dtos.CategoryData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Interface
{
    public interface ICategoryService : ICrudService<Categories, CategoryDto>
    {
        CategoryDto categoryByName(string name);
        int InsertCategoryById(CategoryDto model);
    }
}

using ajansflix.CORE.Repository;
using ajansflix.DOMAIN.Models;
using ajansflix.SERVICES.Crud;
using ajansflix.SERVICES.Dtos.CategoryData;
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
    public class CategoryService : CrudService<Categories, CategoryDto>, ICategoryService
    {
        public CategoryService(IRepository<Categories> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public CategoryDto categoryByName(string name)
        {
            var entity = _repository.Where(x=> x.CategoryName == name).AsNoTracking().SingleOrDefault();
            var entityDto = _mapper.Map<Categories, CategoryDto>(entity);
            return entityDto;
        }

        public int InsertCategoryById(CategoryDto model)
        {
            var entity = _mapper.Map<CategoryDto, Categories>(model);
            _repository.Add(entity);
            _repository.Save();
            model.Id = entity.Id;
            return model.Id;
        }

        public List<CategoryDto> listCategoryByWeb()
        {
            var entity = _repository.Where(x=> x.IsActive == true).OrderBy(x=> x.CategorySorted).ToList();
            var entityDto = _mapper.Map<List<Categories>,List<CategoryDto>>(entity);
            return entityDto;
        }
    }
}

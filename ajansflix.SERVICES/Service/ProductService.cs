using ajansflix.CORE.Repository;
using ajansflix.DOMAIN.Models;
using ajansflix.SERVICES.Crud;
using ajansflix.SERVICES.Dtos.ProductsData;
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
    public class ProductService : CrudService<Products, ProductDto>, IProductService
    {
        public ProductService(IRepository<Products> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public List<ProductDto> listProductToWeb()
        {
            var entity = _repository.Where(x => x.IsActive == true).AsNoTracking().Include("category").OrderByDescending(x=> x.CreatedTime).ToList();
            var entityDto = _mapper.Map<List<Products>, List<ProductDto>>(entity);
            return entityDto;
        }

        public List<ProductDto> listProductToWebForSearch()
        {
            var entity = _repository.Where(x => x.IsActive == true).AsNoTracking().ToList();
            var entityDto = _mapper.Map<List<Products>, List<ProductDto>>(entity);
            return entityDto;
        }

        public ProductDto getProduct(int Id)
        {
            var entity = _repository.Where(x => x.Id == Id).Include("category").AsNoTracking().SingleOrDefault();
            var entityDto = _mapper.Map<Products, ProductDto>(entity);
            return entityDto;
        }

        public List<ProductDto> listProductToRelationalByCategory(int productId, int catId)
        {
            var entity = _repository.Where(x => x.CategoryId == catId && x.Id != productId).AsNoTracking().ToList();
            var entityDto = _mapper.Map<List<Products>, List<ProductDto>>(entity);
            return entityDto;
        }

        public List<ProductDto> productListByCategory(int categoryId)
        {
            var entity = _repository.Where(x => x.CategoryId == categoryId).AsNoTracking().ToList();
            var entityDto = _mapper.Map<List<Products>, List<ProductDto>>(entity);
            return entityDto;
        }

        public List<ProductDto> listProductToAdmin()
        {
            var entity = _repository.Where(x => x.Id > 0).AsNoTracking().Include("category").OrderByDescending(x => x.CreatedTime).ToList();
            var entityDto = _mapper.Map<List<Products>, List<ProductDto>>(entity);
            return entityDto;
        }

        public int InsertProductById(ProductDto model)
        {
            var entity = _mapper.Map<ProductDto, Products>(model);
            _repository.Add(entity);
            _repository.Save();
            model.Id = entity.Id;
            return model.Id;
        }
    }
}

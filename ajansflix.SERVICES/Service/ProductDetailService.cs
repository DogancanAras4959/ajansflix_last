using ajansflix.CORE.Repository;
using ajansflix.DOMAIN.Models;
using ajansflix.SERVICES.Crud;
using ajansflix.SERVICES.Dtos.ProductDetailsData;
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
    public class ProductDetailService : CrudService<ProductDetails, ProductDetailDto>, IProductDetailService
    {
        public ProductDetailService(IRepository<ProductDetails> repository, IMapper mapper) : base(repository, mapper)
        {
            
        }

        public ProductDetailDto getComponent(int Id, int ProductId)
        {
            var entity = _repository.Where(x => x.DetailId == Id && x.ProductId == ProductId).SingleOrDefault();
            var entityDto = _mapper.Map<ProductDetails, ProductDetailDto>(entity);
            return entityDto;
        }

        public List<ProductDetailDto> listDetails(int productId)
        {
            var entity = _repository.Where(x => x.ProductId == productId && x.IsActive == true).AsNoTracking().Include("product").Include("detail").OrderByDescending(x => x.CreatedTime).ToList();
            var entityDto = _mapper.Map<List<ProductDetails>, List<ProductDetailDto>>(entity);
            return entityDto;
        }

        public List<ProductDetailDto> listToAdmin()
        {
            var entity = _repository.Where(x => x.Id > 0).AsNoTracking().Include("product").Include("detail").OrderByDescending(x => x.CreatedTime).ToList();
            var entityDto = _mapper.Map<List<ProductDetails>, List<ProductDetailDto>>(entity);
            return entityDto;
        }
    }
}

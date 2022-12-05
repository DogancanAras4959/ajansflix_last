using ajansflix.DOMAIN.Models;
using ajansflix.SERVICES.Crud;
using ajansflix.SERVICES.Dtos.ProductDetailsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Interface
{
    public interface IProductDetailService : ICrudService<ProductDetails, ProductDetailDto>
    {
        List<ProductDetailDto> listDetails(int productId);
        ProductDetailDto getComponent(int Id, int ProductId);
        List<ProductDetailDto> listToAdmin();
    }
}

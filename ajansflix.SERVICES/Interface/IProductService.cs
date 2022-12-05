using ajansflix.DOMAIN.Models;
using ajansflix.SERVICES.Crud;
using ajansflix.SERVICES.Dtos.ProductsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Interface
{
    public interface IProductService : ICrudService<Products, ProductDto>
    {
        List<ProductDto> listProductToAdmin();
        List<ProductDto> listProductToWeb();
        List<ProductDto> listProductToWebForSearch();
        ProductDto getProduct(int Id);
        List<ProductDto> listProductToRelationalByCategory(int productId, int catId);

        List<ProductDto> productListByCategory(int categoryId);

        int InsertProductById(ProductDto model);
    }
}

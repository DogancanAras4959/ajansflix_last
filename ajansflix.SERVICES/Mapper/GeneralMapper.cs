using ajansflix.DOMAIN.Models;
using ajansflix.SERVICES.Dtos.CategoryData;
using ajansflix.SERVICES.Dtos.CustomerData;
using ajansflix.SERVICES.Dtos.DetailData;
using ajansflix.SERVICES.Dtos.DetailDataData;
using ajansflix.SERVICES.Dtos.FavaoriteProductData;
using ajansflix.SERVICES.Dtos.OrderProductsData;
using ajansflix.SERVICES.Dtos.OrdersData;
using ajansflix.SERVICES.Dtos.ProductDetailsData;
using ajansflix.SERVICES.Dtos.ProductsData;
using ajansflix.SERVICES.Dtos.QuantityCompData;
using ajansflix.SERVICES.Dtos.RoleData;
using ajansflix.SERVICES.Dtos.UserData;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Mapper
{
    public class GeneralMapper : Profile
    {
        public GeneralMapper()
        {
            CreateMap<Roles, RoleDto>();
            CreateMap<RoleDto, Roles>();

            CreateMap<Users, UserDto>().ForMember(x => x.role, y => y.MapFrom(t => t.role));
            CreateMap<UserDto, Users>();

            CreateMap<VideosData, VideoDataDto>().ForMember(x => x.product, y => y.MapFrom(t => t.product));
            CreateMap<VideoDataDto, VideosData>();

            CreateMap<Categories, CategoryDto>();
            CreateMap<CategoryDto, Categories>();

            CreateMap<Customers, CustomerDto>();
            CreateMap<CustomerDto, Customers>();

            CreateMap<QuantityComponent, QuantityComponentDto>().ForMember(x=> x.product, y=> y.MapFrom(t=> t.product));
            CreateMap<QuantityComponentDto, QuantityComponent>();

            CreateMap<Details, DetailDto>();
            CreateMap<DetailDto, Details>();

            CreateMap<DetailsData, DetailDataDto>().ForMember(x=> x.detail, y=> y.MapFrom(t=> t.detail));
            CreateMap<DetailDataDto, DetailsData>();

            CreateMap<ProductDetails, ProductDetailDto>()
                .ForMember(x=> x.detail, y=> y.MapFrom(t=> t.detail))
                .ForMember(x=> x.product, y=> y.MapFrom(t=> t.product));
            CreateMap<ProductDetailDto, ProductDetails>(); 
            
            CreateMap<Products, ProductDto>().ForMember(x=> x.category, y=> y.MapFrom(t => t.category));
            CreateMap<ProductDto, Products>();

            CreateMap<FavoriteProducts, FavoriteProductDto>()
                .ForMember(x => x.customer, y => y.MapFrom(t => t.customer))
                .ForMember(x => x.product, y => y.MapFrom(t => t.product));
            CreateMap<FavoriteProductDto, FavoriteProducts>(); 
            
            CreateMap<OrderProducts, OrderProductDto>()
                .ForMember(x=> x.order, y=> y.MapFrom(t=> t.order))
                .ForMember(x=> x.product, y=> y.MapFrom(t => t.product));
            CreateMap<OrderProductDto, OrderProducts>();

            CreateMap<Orders, OrdersDto>()
                .ForMember(x=> x.customer, y=> y.MapFrom(t=> t.customer))
                .ForMember(x=> x.orderStatus, y=> y.MapFrom(t=> t.orderStatus));
            CreateMap<OrdersDto, Orders>();

            CreateMap<OrderStatus, OrderStatusDto>();
            CreateMap<OrderStatusDto, OrderStatus>();
        }
    }
}

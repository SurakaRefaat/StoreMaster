using AutoMapper;
using Store.Data.Entities;

namespace Store.Service.Services.ProductService.Dtos
{
    public  class ProductProfile :Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProuductDetailsDto>()
                .ForMember(dest => dest.Brandname,options => options.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.TypeName, options => options.MapFrom(src => src.Type.Name))
                .ForMember(dest => dest.pictureUrl, options => options.MapFrom<ProductPictureUrlResolver>());

            CreateMap<ProductBrand, BrandTypeDetailsDto>();
            CreateMap<ProductType, BrandTypeDetailsDto>();

        }
    }
}

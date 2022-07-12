using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace Service.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            ////Expression<Func<ProductToReturnDto, string>> myExpression = d => d.PictureUrl;
            ////Func<ProductToReturnDto, string> myExpression = d => d.PictureUrl;

            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, ProductTypeName)
                .ForMember(d => d.County, o => o.MapFrom(s => s.County.CountyName))
                .ForMember(d => d.City, o => o.MapFrom(s => s.County.City.CityName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<AppUser, MemberDto>();
            CreateMap<Photo, PhotoDto>()
                .ForMember(d => d.Url, o => o.MapFrom<PhotoUrlResolver>());
        }

        public static void ProductTypeName(IMemberConfigurationExpression<Product, ProductToReturnDto, string> mem)
        {
            mem.MapFrom(s => s.ProductType.Name);
        }
    }
}
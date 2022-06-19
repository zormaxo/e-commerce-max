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
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>())
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src =>
                    src.Photos.FirstOrDefault(x => x.IsMain).Url));

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
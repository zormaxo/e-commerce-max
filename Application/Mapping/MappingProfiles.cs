using Application.Entities;
using AutoMapper;
using Core.Dtos;
using Core.Entities;

namespace Application.Mapping;

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
            .ForMember(d => d.IsNew, o => o.MapFrom(s => s.ProductMachine.IsNew))
            .ForMember(d => d.Category, o => o.MapFrom(s => s.Category))
            .ForMember(d => d.PriceTRY, o => o.MapFrom<CurrencyResolver>())
            .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

        CreateMap<AppUser, MemberDto>();
        CreateMap<Photo, PhotoDto>()
            .ForMember(d => d.Url, o => o.MapFrom<PhotoUrlResolver>());
        CreateMap<CurrencyDto, Currency>()
            .ForMember(d => d.Eur, o => o.MapFrom(s => s.Rates.EUR))
            .ForMember(d => d.Try, o => o.MapFrom(s => s.Rates.TRY))
            .ForMember(d => d.Gbp, o => o.MapFrom(s => s.Rates.GBP))
            .ForMember(d => d.Usd, o => o.MapFrom(s => s.Rates.USD));
    }

    public static void ProductTypeName(IMemberConfigurationExpression<Product, ProductToReturnDto, string> mem)
    {
        mem.MapFrom(s => s.Category.Name);
    }
}
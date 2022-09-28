using Application.Entities;
using AutoMapper;
using Core.Dtos;
using Core.Dtos.Member;
using Core.Entities;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Product;

namespace Application.Mapping;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        ////Expression<Func<ProductToReturnDto, string>> myExpression = d => d.PictureUrl;
        ////Func<ProductToReturnDto, string> myExpression = d => d.PictureUrl;

        CreateMap<Product, ProductToReturnDto>();
        //.ForMember(d => d.PriceText, o => o.MapFrom<CurrencyResolver>())
        //.ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

        CreateMap<Product, ShowcaseDto>()
            .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain).Url));

        CreateMap<Product, ProductDetailDto>();

        CreateMap<ProductDetailDto, ProductDetailDto>();

        CreateMap<AppUser, ProductMemberDto>();




        CreateMap<AppUser, MemberDto>()
                 .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src =>
                     src.Photos.FirstOrDefault(x => x.IsMain).Url));

        CreateMap<ProductPhoto, PhotoDto>()
            .ForMember(d => d.Url, o => o.MapFrom<PhotoUrlResolver>());

        CreateMap<UserPhoto, PhotoDto>();

        CreateMap<CurrencyDto, Currency>()
            .ForMember(d => d.Eur, o => o.MapFrom(s => s.Rates.EUR))
            .ForMember(d => d.Try, o => o.MapFrom(s => s.Rates.TRY))
            .ForMember(d => d.Gbp, o => o.MapFrom(s => s.Rates.GBP))
            .ForMember(d => d.Usd, o => o.MapFrom(s => s.Rates.USD));

        //CreateMap<Product, Product>()
        //     .IgnoreAllMembers()
        //     .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));

        CreateMap<MemberUpdateDto, AppUser>() //in order not to rewrite null values over existings
             .ForAllMembers(opts => opts.Condition((_, __, srcMember) => srcMember != null));
        CreateMap<MemberNameUpdateDto, MemberUpdateDto>();
        CreateMap<MemberPhoneUpdateDto, MemberUpdateDto>();
        CreateMap<MemberUsernameUpdateDto, MemberUpdateDto>();

        CreateMap<Category, CategoryDto>();
        CreateMap<County, CountyDto>();
        CreateMap<City, CityDto>();



    }

    public static void ProductTypeName(IMemberConfigurationExpression<Product, ProductToReturnDto, string> mem)
    {
        mem.MapFrom(s => s.Category.Name);
    }
}
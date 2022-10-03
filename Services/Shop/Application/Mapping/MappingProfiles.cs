using Application.Entities;
using AutoMapper;
using Core.Dtos;
using Core.Dtos.Member;
using Core.Entities;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Product;
using Shop.Core.Extensions;

namespace Application.Mapping;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductToReturnDto>();
        //.ForMember(d => d.PriceText, o => o.MapFrom<CurrencyResolver>())
        //.ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

        CreateMap<Product, ShowcaseDto>()
            .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain).Url));

        CreateMap<Product, ProductMachineDto>()
            .ForMember(dest => dest.PriceText, opt => opt.MapFrom(src =>
                     src.Price.ToString().ToPriceText(src.Currency)))
            .ForMember(d => d.CreatedDate, o => o.MapFrom(src =>
                     src.CreatedDate.ToString("dd.MM.yyyy")))
            .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain).Url));

        CreateMap<Product, ProductMaterialDto>()
            .ForMember(dest => dest.PriceText, opt => opt.MapFrom(src =>
                     src.Price.ToString().ToPriceText(src.Currency)))
            .ForMember(d => d.CreatedDate, o => o.MapFrom(src =>
                     src.CreatedDate.ToString("d")))
            .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain).Url));

        CreateMap<Product, ProductDetailDto>()
            .ForMember(dest => dest.PriceText, opt => opt.MapFrom(src =>
                     src.Price.ToString().ToPriceText(src.Currency)))
            .ForMember(d => d.CreatedDate, o => o.MapFrom(src =>
                     src.CreatedDate.ToString("d")));

        CreateMap<AppUser, ProductMemberDto>()
            .ForMember(d => d.PhotoUrl, o => o.MapFrom(src =>
                     src.Photos.FirstOrDefault(x => x.IsMain).Url))
            .ForMember(d => d.CreatedDate, o => o.MapFrom(src =>
                     src.CreatedDate.ToString("d")));

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

        CreateMap<MemberUpdateDto, AppUser>() //in order not to rewrite null values over existings
             .ForAllMembers(opts => opts.Condition((_, __, srcMember) => srcMember != null));
        CreateMap<MemberNameUpdateDto, MemberUpdateDto>();
        CreateMap<MemberPhoneUpdateDto, MemberUpdateDto>();
        CreateMap<MemberUsernameUpdateDto, MemberUpdateDto>();

        CreateMap<Category, CategoryDto>();
        CreateMap<County, CountyDto>();
        CreateMap<City, CityDto>();
    }
}
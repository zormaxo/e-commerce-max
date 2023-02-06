using AutoMapper;
using Shop.Core.Entities;
using Shop.Core.Entities.Identity;
using Shop.Core.Entities.OrderAggregate;
using Shop.Core.Extensions;
using Shop.Shared.Dtos;
using Shop.Shared.Dtos.Account;
using Shop.Shared.Dtos.Basket;
using Shop.Shared.Dtos.City;
using Shop.Shared.Dtos.Member;
using Shop.Shared.Dtos.Product;

namespace Shop.Application.Common.Helpers.AutoMapperHelper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductDetailDto>()
            .ForMember(d => d.PriceText, opt => opt.MapFrom(src => src.Price.ToString().ToPriceText(src.Currency)))
            .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain).Url));

        CreateMap<Product, ProductVehicleDto>()
            .ForMember(dest => dest.PriceText, opt => opt.MapFrom(src => src.Price.ToString().ToPriceText(src.Currency)))
            .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain).Url));

        CreateMap<Product, ProductComputerDto>()
            .ForMember(dest => dest.PriceText, opt => opt.MapFrom(src => src.Price.ToString().ToPriceText(src.Currency)))
            .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain).Url));

        CreateMap<Product, ProductRealEstateDto>()
            .ForMember(dest => dest.PriceText, opt => opt.MapFrom(src => src.Price.ToString().ToPriceText(src.Currency)))
            .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain).Url));

        CreateMap<AppUser, ProductMemberDto>()
            .ForMember(d => d.PhotoUrl, o => o.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
            .ForMember(d => d.CreatedDate, o => o.MapFrom(src => src.CreatedDate.ToString("d")));

        CreateMap<AppUser, MemberDto>()
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

        CreateMap<ProductPhoto, PhotoDto>().ForMember(d => d.Url, o => o.MapFrom<PhotoUrlResolver>());

        CreateMap<UserPhoto, PhotoDto>();

        CreateMap<CurrencyFreakDto, Currency>()
            .ForMember(d => d.Eur, o => o.MapFrom(s => s.Rates.EUR))
            .ForMember(d => d.Try, o => o.MapFrom(s => s.Rates.TRY))
            .ForMember(d => d.Gbp, o => o.MapFrom(s => s.Rates.GBP))
            .ForMember(d => d.Usd, o => o.MapFrom(s => s.Rates.USD));

        CreateMap<MemberUpdateDto, AppUser>() //in order not to rewrite null values over existings
            .ForAllMembers(opts => opts.Condition((_, __, srcMember) => srcMember != null));
        CreateMap<MemberNameUpdateDto, MemberUpdateDto>();
        CreateMap<MemberPhoneUpdateDto, MemberUpdateDto>();
        CreateMap<MemberEmailUpdateDto, MemberUpdateDto>();

        CreateMap<Category, CategoryDto>();
        CreateMap<County, CountyDto>();
        CreateMap<City, CityWithCountyDto>();
        CreateMap<City, CityDto>();

        CreateMap<RegisterDto, AppUser>();
        CreateMap<Favourite, FavouriteDto>().ReverseMap();

        CreateMap<Message, MessageDto>()
            .ForMember(
                dest => dest.SenderPhotoUrl,
                opt => opt.MapFrom(src => src.Sender.Photos.FirstOrDefault(x => x.IsMain).Url))
            .ForMember(
                dest => dest.RecipientPhotoUrl,
                opt => opt.MapFrom(src => src.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url));

        CreateMap<CustomerBasketDto, CustomerBasket>();
        CreateMap<BasketItemDto, BasketItem>();

        CreateMap<DateTime, DateTime>().ConvertUsing(d => DateTime.SpecifyKind(d, DateTimeKind.Utc));
        CreateMap<DateTime?, DateTime?>().ConvertUsing(d => d.HasValue ? DateTime.SpecifyKind(d.Value, DateTimeKind.Utc) : null);

        CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>().ReverseMap();
        CreateMap<Order, OrderToReturnDto>()
            .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
            .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));
        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
            .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
            .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PictureUrl));
        //.ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());

        CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();
    }
}
//using Application.Entities;
//using AutoMapper;
//using Core.Dtos;
//using Core.Entities;
//using Microsoft.Extensions.Configuration;

//namespace Application.Mapping;

//public class CurrencyResolver : IValueResolver<CurrencyDto, Currency>
//{
//    private readonly IConfiguration _config;

//    public CurrencyResolver(IConfiguration config)
//    {
//        _config = config;
//    }

//    public double Resolve(Product source, ProductToReturnDto destination, double destMember, ResolutionContext context)
//    {
//        var url = source.Photos.FirstOrDefault(x => x.IsMain)?.Url;
//        if (!string.IsNullOrEmpty(url))
//        {
//            bool isValidUrl = Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
//                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

//            if (isValidUrl)
//                return url;

//            return _config["ApiUrl"] + url;
//        }
//        return null;
//    }

//    public void Resolve(CurrencyDto source, Currency destination, double destMember, ResolutionContext context)
//    {
//        destination.Try = Convert.ToDouble($"{source.Rates.TRY:F2}"); 
//        destination.Eur = Convert.ToDouble($"{source.Rates.EUR:F2}"); 
//        destination.Gbp = Convert.ToDouble($"{source.Rates.GBP:F2}"); 
//        destination.Usd = Convert.ToDouble($"{source.Rates.USD:F2}");
//    }

//    public void Resolve(CurrencyDto source, Currency destination, void destMember, ResolutionContext context)
//    {
//        throw new NotImplementedException();
//    }
//}
using Application.Entities;
using AutoMapper;
using Core.Dtos;
using Microsoft.Extensions.Configuration;

namespace Application.Mapping;

public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
{
    private readonly IConfiguration _config;

    public ProductUrlResolver(IConfiguration config)
    {
        _config = config;
    }

    public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
    {
        var url = source.Photos.FirstOrDefault(x => x.IsMain)?.Url;
        if (!string.IsNullOrEmpty(url))
        {
            bool isValidUrl = Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (isValidUrl)
                return url;

            return _config["ApiUrl"] + url;
        }
        return null;
    }
}
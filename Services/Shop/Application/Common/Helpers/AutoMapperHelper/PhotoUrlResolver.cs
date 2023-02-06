using AutoMapper;
using Microsoft.Extensions.Configuration;
using Shop.Core.Entities;
using Shop.Shared.Dtos;

namespace Shop.Application.Common.Helpers.AutoMapperHelper;

public class PhotoUrlResolver : IValueResolver<ProductPhoto, PhotoDto, string>
{
    private readonly IConfiguration _config;

    public PhotoUrlResolver(IConfiguration config) { _config = config; }

    public string Resolve(ProductPhoto source, PhotoDto destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.Url))
        {
            bool isValidUrl = Uri.TryCreate(source.Url, UriKind.Absolute, out Uri uriResult) &&
                (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (isValidUrl)
                return source.Url;

            return _config["ApiUrl"] + source.Url;
        }
        return null;
    }
}
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
        var url = source.Photos.FirstOrDefault(x => x.IsMain).Url;
        if (!string.IsNullOrEmpty(url))
        {
            return _config["ApiUrl"] + url;
        }
        return null;
    }
}
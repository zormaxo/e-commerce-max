using System.Linq.Expressions;
using AutoMapper;
using Core.Dtos;
using Core.Entities;

namespace Service.Helpers
{
  public class MappingProfiles : Profile
  {

    public MappingProfiles()
    {
      Expression<Func<ProductToReturnDto, string>> myExpression = d => d.PictureUrl;
      Func<ProductToReturnDto, string> omer = d => d.PictureUrl;

      CreateMap<Product, ProductToReturnDto>()
          .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
          .ForMember(d => d.ProductType, MyMethod)
          .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
    }

    public void MyMethod(IMemberConfigurationExpression<Product, ProductToReturnDto, string> mem)
    {
      mem.MapFrom(s => s.ProductType.Name);
    }
  }
}
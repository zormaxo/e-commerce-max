using Core.Entities;
using Core.Specification;

namespace Core.Specifications
{
  public class ProductsWithFiltersForCountSpecification : BaseSpecifcation<Product>
  {
    public ProductsWithFiltersForCountSpecification(ProductSpecParams productParams)
      : base(new ProductSpecParamsToCriteria(productParams).GetCriteria())
    {

    }
  }
}

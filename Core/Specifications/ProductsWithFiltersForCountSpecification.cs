using Application.Entities;
using Application.Specification;

namespace Application.Specifications
{
    public class ProductsWithFiltersForCountSpecification : BaseSpecifcation<Product>
    {
        public ProductsWithFiltersForCountSpecification(ProductSpecParams productParams)
          : base(new ProductSpecParamsToCriteria(productParams).GetCriteria())
        {
        }
    }
}
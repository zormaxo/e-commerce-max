using Application.Entities;
using Application.Specification;
using Microsoft.EntityFrameworkCore;

namespace Application.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecifcation<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams)
          : base(new ProductSpecParamsToCriteria(productParams).GetCriteria())
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.Photos);
            AddInclude(x => x.Include(x => x.County).ThenInclude(x => x.City));
            //AddInclude("County.City");
            AddOrderBy(x => x.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;

                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;

                    case "dateAsc":
                        AddOrderBy(p => p.Created);
                        break;

                    case "dateDesc":
                        AddOrderByDescending(p => p.Created);
                        break;

                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Include(y => y.Category));
            AddInclude(x => x.Include(x => x.ProductBrand));
            AddInclude(x => x.Include(x => x.Photos));
        }
    }
}
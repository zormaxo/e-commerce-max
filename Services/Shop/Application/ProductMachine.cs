using Application;
using Application.Specifications;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Shop.Application
{
    public static class ProductMachineHelper
    {
        public static void FilterProduct(IQueryable<Product> filteredProducts, ProductSpecParams productParams)
        {
            filteredProducts.Include(x => x.ProductMachine)
                .WhereIf(productParams.IsNew.HasValue, p => p.ProductMachine.IsNew == productParams.IsNew);
        }
    }
}

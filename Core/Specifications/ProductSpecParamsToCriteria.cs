using Core.Entities;
using Core.Specifications;
using System.Linq.Expressions;

namespace Core.Specification
{
    public class ProductSpecParamsToCriteria
    {
        private readonly ProductSpecParams _specParam;

        public ProductSpecParamsToCriteria(ProductSpecParams productSpecificationParameters)
        {
            _specParam = productSpecificationParameters;
        }

        public Expression<Func<Product, bool>> GetCriteria() =>
            p => (string.IsNullOrEmpty(_specParam.Search) || p.Name.ToLower().Contains(_specParam.Search)) &&
                 (!_specParam.BrandId.HasValue || p.ProductBrandId == _specParam.BrandId) &&
                 (!_specParam.TypeId.HasValue || p.ProductTypeId == _specParam.TypeId) &&
                 (!_specParam.TypeId.HasValue || p.ProductTypeId == _specParam.TypeId) &&
                 (!_specParam.IsNew.HasValue || p.IsNew == _specParam.IsNew) &&
                 (!_specParam.MaxValue.HasValue || p.Price < _specParam.MaxValue) &&
                 (!_specParam.MinValue.HasValue || p.Price > _specParam.MinValue) &&
                 (p.IsActive);
    }
}
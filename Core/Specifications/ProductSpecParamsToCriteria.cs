using Application.Entities;
using Application.Specifications;
using System.Linq.Expressions;

namespace Application.Specification
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
                 //(!_specParam.CategoryId.HasValue || p.CategoryId == _specParam.CategoryId) &&
                 //(!_specParam.CategoryId.HasValue || p.CategoryId == _specParam.CategoryId) &&
                 //(!_specParam.IsNew.HasValue || p.IsNew == _specParam.IsNew) &&
                 (!_specParam.MaxValue.HasValue || p.Price < _specParam.MaxValue) &&
                 (!_specParam.MinValue.HasValue || p.Price > _specParam.MinValue) &&
                 (!_specParam.UserId.HasValue || p.UserId == _specParam.UserId) &&
                 (_specParam.GetAllStatus.HasValue || p.IsActive) &&  //true: All, false: InActive, null: Active
                 (!(_specParam.GetAllStatus.HasValue && _specParam.GetAllStatus == false) || !p.IsActive);
    }
}
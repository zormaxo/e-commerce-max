using Core.HelperTypes;
using Infrastructure;
using Shop.Core.HelperTypes;

namespace Shop.Application.Helpers
{
    public class ProductHelper
    {
        private CachedItems _cachedItems;

        public ProductHelper(CachedItems cachedItems) { _cachedItems = cachedItems; }

        public void CalculateMaxMinVal(ProductParams productParams)
        {
            if(productParams.MinValue.HasValue)
            {
                productParams.MinValue = productParams.Currency switch
                {
                    CurrencyCode.USD => (int)((decimal)productParams.MinValue * (int)_cachedItems.Currency.Try),
                    CurrencyCode.EUR => (int)((decimal)productParams.MinValue /
                        (int)_cachedItems.Currency.Eur *
                        (int)_cachedItems.Currency.Try),
                    CurrencyCode.GBP => (int)((decimal)productParams.MinValue /
                        (int)_cachedItems.Currency.Gbp *
                        (int)_cachedItems.Currency.Try),
                    CurrencyCode.TRY => (int)((decimal)productParams.MinValue),
                    _ => productParams.MinValue,
                };
            }

            if(productParams.MaxValue.HasValue)
            {
                productParams.MaxValue = productParams.Currency switch
                {
                    CurrencyCode.USD => (int)((decimal)productParams.MaxValue * _cachedItems.Currency.Try),
                    CurrencyCode.EUR => (int)((decimal)productParams.MaxValue /
                        _cachedItems.Currency.Eur *
                        _cachedItems.Currency.Try),
                    CurrencyCode.GBP => (int)((decimal)productParams.MaxValue /
                        _cachedItems.Currency.Gbp *
                        _cachedItems.Currency.Try),
                    CurrencyCode.TRY => (int)((decimal)productParams.MaxValue),
                    _ => productParams.MaxValue,
                };
            }
        }
    }
}

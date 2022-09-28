using Core.HelperTypes;

namespace Shop.Core.Extensions
{
    public static class StringExtension
    {
        public static string ToPriceText(this string str, CurrencyCode currencyCode)
        {
            return currencyCode switch
            {
                CurrencyCode.USD => $"{str} USD",
                CurrencyCode.EUR => $"{str} EUR",
                CurrencyCode.GBP => $"{str} GBP",
                CurrencyCode.TRY => $"{str} TL",
                _ => $"{str:n} TL",
            };
        }
    }
}

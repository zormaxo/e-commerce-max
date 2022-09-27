namespace Shop.Application.Helpers
{
    public static class CategoryNameHelper
    {
        public static string GetCategoryEagerName(string categoryName)
        {

            return categoryName switch
            {
                "makina" => "ProductMachine",
                "malzeme" => "ProductMaterial",
                _ => "",
            };
        }
    }
}

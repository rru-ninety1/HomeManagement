using HomeManagement.Client.Features.Catalog.ProductCategories.Models;

namespace HomeManagement.Client.Features.Catalog.ProductCategories.Store
{
    public class ProductCategoryLoadAction { }

    public class ProductCategorySetCategoriesAction
    {
        public ProductCategory[] Categories { get; }

        public ProductCategorySetCategoriesAction(ProductCategory[] categories)
        {
            Categories = categories;
        }
    }
}

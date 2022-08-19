using Fluxor;

namespace HomeManagement.Client.Features.Catalog.ProductCategories.Store
{
    public static class ProductCategoryReducers
    {
        [ReducerMethod(typeof(ProductCategoryLoadAction))]
        public static ProductCategoryState OnLoad(ProductCategoryState state) => state with { Loading = true };

        [ReducerMethod]
        public static ProductCategoryState OnSetCategories(ProductCategoryState state, ProductCategorySetCategoriesAction action) => state with { Initialized = true, Loading = false, Categories = action.Categories };
    }
}

using Fluxor;
using HomeManagement.Client.Features.Catalog.ProductCategories.Models;
using HomeManagement.Client.Features.Services.IndexedDB;

namespace HomeManagement.Client.Features.Catalog.ProductCategories.Store
{
    [FeatureState(Name = "ProductCategoryState")]
    public record class ProductCategoryState
    {
        public bool Initialized { get; init; }
        public bool Loading { get; init; }
        public ProductCategory[] Categories { get; init; }

        public ProductCategoryState() => (Initialized, Loading, Categories) = (false, false, Array.Empty<ProductCategory>());
    }

    #region Actions

    public class ProductCategoryLoadAction { }

    public class ProductCategorySetCategoriesAction
    {
        public ProductCategory[] Categories { get; }

        public ProductCategorySetCategoriesAction(ProductCategory[] categories)
        {
            Categories = categories;
        }
    }

    #endregion

    #region Reducers

    public static class ProductCategoryReducers
    {
        [ReducerMethod(typeof(ProductCategoryLoadAction))]
        public static ProductCategoryState OnLoad(ProductCategoryState state) => state with { Loading = true };

        [ReducerMethod]
        public static ProductCategoryState OnSetCategories(ProductCategoryState state, ProductCategorySetCategoriesAction action) => state with { Initialized = true, Loading = false, Categories = action.Categories };
    }

    #endregion

    #region Effects

    public class ProductCategoryEffects
    {
        private readonly HomeManagementIndexedDb _homeManagementIndexedDb;

        public ProductCategoryEffects(HomeManagementIndexedDb homeManagementIndexedDb)
        {
            _homeManagementIndexedDb = homeManagementIndexedDb;
        }

        [EffectMethod(typeof(ProductCategoryLoadAction))]
        public async Task LoadProductCategories(IDispatcher dispacher)
        {
            var categories = await _homeManagementIndexedDb.GetAllItems<ProductCategory>().ConfigureAwait(false);

            dispacher.Dispatch(new ProductCategorySetCategoriesAction(categories.ToArray()));
        }
    }

    #endregion
}

using Fluxor;

namespace HomeManagement.Client.Features.Catalog.Products.Store
{
    public static class ProductReducers
    {
        [ReducerMethod(typeof(ProductLoadAction))]
        public static ProductState OnLoad(ProductState state) => state with { Loading = true };

        [ReducerMethod]
        public static ProductState OnSetProducts(ProductState state, ProductSetProductsAction action) => state with { Initialized = true, Loading = false, Products = action.Products };
    }
}

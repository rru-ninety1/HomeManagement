using Fluxor;

namespace HomeManagement.Client.Features.Loader.Store
{
    public static class LoaderReducers
    {
        [ReducerMethod(typeof(LoaderSetProductCategoriesLoadedAction))]
        public static LoaderState OnSetProductCategoriesLoaded(LoaderState state) => state with { ProductCategoriesLoaded = true };
    }
}

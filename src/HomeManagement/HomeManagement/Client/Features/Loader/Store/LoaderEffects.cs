using Fluxor;
using HomeManagement.Client.Features.Catalog.ProductCategories.Store;
using HomeManagement.Client.Features.Catalog.Products.Store;

namespace HomeManagement.Client.Features.Loader.Store
{
    public class LoaderEffects
    {
        private readonly IState<LoaderState> _loaderState;

        public LoaderEffects(IState<LoaderState> loaderState)
        {
            _loaderState = loaderState;
        }

        [EffectMethod(typeof(LoaderStartLoadingAction))]
        public Task StartLoading(IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new ProductCategoryLoadAction());
            dispatcher.Dispatch(new ProductLoadAction());

            return Task.CompletedTask;
        }

        [EffectMethod(typeof(ProductCategorySetCategoriesAction))]
        public Task SetProductCategoryLoaded(IDispatcher dispatcher)
        {
            if (!_loaderState.Value.Initialized)
            {
                dispatcher.Dispatch(new LoaderSetProductCategoriesLoadedAction());
            }

            return Task.CompletedTask;
        }

        [EffectMethod(typeof(ProductSetProductsAction))]
        public Task SetProductsLoaded(IDispatcher dispatcher)
        {
            if (!_loaderState.Value.Initialized)
            {
                dispatcher.Dispatch(new LoaderSetProductsLoadedAction());
            }

            return Task.CompletedTask;
        }
    }
}

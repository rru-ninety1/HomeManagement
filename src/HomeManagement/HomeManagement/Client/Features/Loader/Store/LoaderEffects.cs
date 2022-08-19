using Fluxor;
using HomeManagement.Client.Features.Catalog.ProductCategories.Store;

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
            //TODO dispatch di altre action

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
    }
}

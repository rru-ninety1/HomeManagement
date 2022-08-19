using Fluxor;

namespace HomeManagement.Client.Features.Loader.Store
{
    [FeatureState]
    public record class LoaderState
    {
        public bool ProductCategoriesLoaded { get; init; }
        public bool Initialized => ProductCategoriesLoaded; //TODO altre condizioni

        public LoaderState() => (ProductCategoriesLoaded) = (false);
    }
}

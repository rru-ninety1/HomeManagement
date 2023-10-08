using Fluxor;

namespace HomeManagement.Client.Features.Loader.Store
{
    [FeatureState(Name = "LoaderState")]
    public record class LoaderState
    {
        public bool ProductCategoriesLoaded { get; init; }
        public bool ProductsLoaded { get; init; }
        public bool Initialized => ProductCategoriesLoaded && ProductsLoaded;


        public LoaderState() => (ProductCategoriesLoaded, ProductsLoaded) = (false, false);
    }
}

using Fluxor;
using HomeManagement.Client.Features.Catalog.Products.Models;

namespace HomeManagement.Client.Features.Catalog.Products.Store
{
    [FeatureState(Name = "ProductState")]
    public record class ProductState
    {
        public bool Initialized { get; init; }
        public bool Loading { get; init; }
        public Product[] Products { get; init; }

        public ProductState() => (Initialized, Loading, Products) = (false, false, Array.Empty<Product>());
    }
}

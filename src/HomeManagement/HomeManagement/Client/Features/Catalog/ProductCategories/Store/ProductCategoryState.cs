using Fluxor;
using HomeManagement.Client.Features.Catalog.ProductCategories.Models;

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
}

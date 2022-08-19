using Fluxor;
using HomeManagement.Client.Features.Catalog.ProductCategories.Models;

namespace HomeManagement.Client.Features.Catalog.ProductCategories.EditProductCategory.Store
{
    [FeatureState(Name = "EditProductCategoryState")]
    public record class EditProductCategoryState
    {
        public bool Visibile { get; init; }
        public bool Saving { get; init; }
        public ProductCategory Model { get; init; }
    }
}

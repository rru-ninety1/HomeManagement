using Fluxor;
using HomeManagement.Client.Features.Catalog.ProductCategories.Models;

namespace HomeManagement.Client.Features.Catalog.ProductCategories.Store
{
    public class ProductCategoryEffects
    {
        public ProductCategoryEffects()
        {
            //TODO DI service
        }

        [EffectMethod(typeof(ProductCategoryLoadAction))]
        public async Task LoadProductCategories(IDispatcher dispacher)
        {
            //TODO leggere le categorie
            ProductCategory[] categories = new ProductCategory[] {
                new ProductCategory { Id = "1", Description = "Cat 1" },
                new ProductCategory {Id="2", Description = "Cat 2"}
            };

            await Task.Delay(2000);

            dispacher.Dispatch(new ProductCategorySetCategoriesAction(categories));
        }
    }
}

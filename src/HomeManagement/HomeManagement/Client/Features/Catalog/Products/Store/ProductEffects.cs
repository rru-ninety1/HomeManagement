using Fluxor;
using HomeManagement.Client.Features.Catalog.Products.Models;

namespace HomeManagement.Client.Features.Catalog.Products.Store
{
    public class ProductEffects
    {
        [EffectMethod(typeof(ProductLoadAction))]
        public async Task LoadProducts(IDispatcher dispacher)
        {
            //TODO leggere i prodotti
            Product[] products = new Product[] {
                new Product { Id = "1", Description = "Prod 1",CategoryId="1" },
                new Product { Id = "2", Description = "Prod 2",CategoryId="2" }

            };

            await Task.Delay(2000);

            dispacher.Dispatch(new ProductSetProductsAction(products));
        }
    }
}

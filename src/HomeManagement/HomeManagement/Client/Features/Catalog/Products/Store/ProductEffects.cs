using Fluxor;
using HomeManagement.Client.Features.Catalog.Products.Models;
using HomeManagement.Client.Features.Services.IndexedDB;

namespace HomeManagement.Client.Features.Catalog.Products.Store
{
    public class ProductEffects
    {
        private readonly HomeManagementIndexedDb _homeManagementIndexedDb;

        public ProductEffects(HomeManagementIndexedDb homeManagementIndexedDb)
        {
            _homeManagementIndexedDb = homeManagementIndexedDb;
        }

        [EffectMethod(typeof(ProductLoadAction))]
        public async Task LoadProducts(IDispatcher dispacher)
        {
            var products = await _homeManagementIndexedDb.GetAllItems<Product>().ConfigureAwait(false);

            dispacher.Dispatch(new ProductSetProductsAction(products.ToArray()));
        }
    }
}

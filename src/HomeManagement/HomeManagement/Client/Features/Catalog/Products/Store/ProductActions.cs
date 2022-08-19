using HomeManagement.Client.Features.Catalog.Products.Models;

namespace HomeManagement.Client.Features.Catalog.Products.Store
{
    public class ProductLoadAction { }

    public class ProductSetProductsAction
    {
        public Product[] Products { get; }

        public ProductSetProductsAction(Product[] products)
        {
            Products = products;
        }
    }
}

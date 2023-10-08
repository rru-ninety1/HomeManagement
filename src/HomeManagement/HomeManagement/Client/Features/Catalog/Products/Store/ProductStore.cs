using Fluxor;
using HomeManagement.Client.Features.Catalog.Products.Models;
using HomeManagement.Client.Features.Services.IndexedDB;

namespace HomeManagement.Client.Features.Catalog.Products.Store;

[FeatureState(Name = "ProductState")]
public record class ProductState
{
    public bool Initialized { get; init; }
    public bool Loading { get; init; }
    public Product[] Products { get; init; }

    public ProductState() => (Initialized, Loading, Products) = (false, false, Array.Empty<Product>());
}

#region Actions

public class ProductLoadAction { }

public class ProductSetProductsAction
{
    public Product[] Products { get; }

    public ProductSetProductsAction(Product[] products)
    {
        Products = products;
    }
}

#endregion

#region Reducers

public static class ProductReducers
{
    [ReducerMethod(typeof(ProductLoadAction))]
    public static ProductState OnLoad(ProductState state) => state with { Loading = true };

    [ReducerMethod]
    public static ProductState OnSetProducts(ProductState state, ProductSetProductsAction action) => state with { Initialized = true, Loading = false, Products = action.Products };
}

#endregion

#region Effects

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

#endregion
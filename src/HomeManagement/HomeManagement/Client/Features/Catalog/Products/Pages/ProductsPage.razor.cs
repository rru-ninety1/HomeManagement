using Fluxor;
using HomeManagement.Client.Features.Catalog.Products.EditProduct.Store;
using HomeManagement.Client.Features.Catalog.Products.Models;
using HomeManagement.Client.Features.Catalog.Products.Store;
using Microsoft.AspNetCore.Components;

namespace HomeManagement.Client.Features.Catalog.Products.Pages;

public partial class ProductsPage
{
    [Inject]
    private IState<ProductState> ProductState { get; set; }

    [Inject]
    private IState<EditProductState> EditProductState { get; set; }

    [Inject]
    public IDispatcher Dispatcher { get; set; }

    private bool Loading => ProductState.Value.Loading;

    private bool Initialized => ProductState.Value.Initialized;

    private bool OnEdit => EditProductState.Value.Visibile;

    private IEnumerable<Product> Products => ProductState.Value.Products;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (!ProductState.Value.Initialized)
        {
            Dispatcher.Dispatch(new ProductLoadAction());
        }
    }

    public void NewProduct()
    {
        Dispatcher.Dispatch(new EditProductAddNewProductAction());
    }

    public void EditProduct(Product product)
    {
        Dispatcher.Dispatch(new EditProductModifyProductAction(product));
    }
}
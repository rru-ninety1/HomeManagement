using Fluxor;
using HomeManagement.Client.Features.Catalog.ProductCategories.Models;
using HomeManagement.Client.Features.Catalog.ProductCategories.Store;
using HomeManagement.Client.Features.Services.IndexedDB;

namespace HomeManagement.Client.Features.Catalog.Products.EditProduct.Store;

[FeatureState(Name = "EditProductState")]
public record class EditProductState
{
    public bool Visibile { get; init; }
    public bool Saving { get; init; }
    public Product Model { get; init; }
}

#region Actions

public class EditProductAddNewProductAction { }

public class EditProductModifyProductAction
{
    public Product Item { get; }

    public EditProductModifyProductAction(Product item)
    {
        Item = item;
    }
}

public class EditProductSaveProductAction
{
    public Product Item { get; }
    public bool IsNew { get; }

    public EditProductSaveProductAction(Product item, bool isNew)
    {
        Item = item;
        IsNew = isNew;
    }
}

public class EditProductEndSavingAction { }

public class EditProductEndEditProductAction { }

#endregion

#region Reducers

public static class EditProductReducers
{
    [ReducerMethod(typeof(EditProductAddNewProductAction))]
    public static EditProductState OnAddNewProduct(EditProductState state) => state with { Visibile = true, Saving = false, Model = new ProductCategory() };

    [ReducerMethod]
    public static EditProductState OnEditProduct(EditProductState state, EditProductModifyProductAction action) => state with { Visibile = true, Saving = false, Model = action.Item };

    [ReducerMethod(typeof(EditProductSaveProductAction))]
    public static EditProductState OnSaveProduct(EditProductState state) => state with { Saving = true };

    [ReducerMethod(typeof(EditProductEndSavingAction))]
    public static EditProductState OnEndSaving(EditProductState state) => state with { Saving = false };

    [ReducerMethod(typeof(EditProductEndEditProductAction))]
    public static EditProductState OnEndEditProduct(EditProductState state) => state with { Visibile = false, Saving = false, Model = null };
}

#endregion

#region Effects

public class EditProductEffects
{
    private readonly HomeManagementIndexedDb _homeManagementIndexedDb;

    public EditProductEffects(HomeManagementIndexedDb homeManagementIndexedDb)
    {
        _homeManagementIndexedDb = homeManagementIndexedDb;
    }

    [EffectMethod]
    public async Task SaveProduct(EditProductSaveProductAction action, IDispatcher dispacher)
    {
        try
        {
            if (action.IsNew)
            {
                var items = new List<Product>();
                items.Add(new Product
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = action.Item.Description,
                    CategoryId = action.Item.CategoryId
                });

                await _homeManagementIndexedDb.AddNewItems<Product>(items).ConfigureAwait(false);
            }
            else
            {
                var items = new List<Product>();
                items.Add(action.Item);

                await _homeManagementIndexedDb.EditItems<Product>(items).ConfigureAwait(false);
            }

            dispacher.Dispatch(new ProductLoadAction());
            dispacher.Dispatch(new EditProductEndEditProductAction());
        }
        catch (Exception ex)
        {
            dispacher.Dispatch(new EditProductEndSavingAction());
        }
    }
}

#endregion

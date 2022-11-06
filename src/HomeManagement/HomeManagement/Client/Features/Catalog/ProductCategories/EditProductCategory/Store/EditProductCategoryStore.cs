using Fluxor;
using HomeManagement.Client.Features.Catalog.ProductCategories.Models;
using HomeManagement.Client.Features.Catalog.ProductCategories.Store;
using HomeManagement.Client.Features.Services.IndexedDB;

namespace HomeManagement.Client.Features.Catalog.ProductCategories.EditProductCategory.Store
{
    [FeatureState(Name = "EditProductCategoryState")]
    public record class EditProductCategoryState
    {
        public bool Visibile { get; init; }
        public bool Saving { get; init; }
        public ProductCategory Model { get; init; }
    }

    #region Actions

    public class EditProductCategoryAddNewProductCategoryAction { }

    public class EditProductCategoryModifyProductCategoryAction
    {
        public ProductCategory Item { get; }

        public EditProductCategoryModifyProductCategoryAction(ProductCategory item)
        {
            Item = item;
        }
    }

    public class EditProductCategorySaveProductCategoryAction
    {
        public ProductCategory Item { get; }
        public bool IsNew { get; }

        public EditProductCategorySaveProductCategoryAction(ProductCategory item, bool isNew)
        {
            Item = item;
            IsNew = isNew;
        }
    }

    public class EditProductCategoryEndSavingAction { }

    public class EditProductCategoryEndEditProductCategoryAction { }

    #endregion

    #region Reducers

    public static class EditProductCategoryReducers
    {
        [ReducerMethod(typeof(EditProductCategoryAddNewProductCategoryAction))]
        public static EditProductCategoryState OnAddNewProductCategory(EditProductCategoryState state) => state with { Visibile = true, Saving = false, Model = new ProductCategory() };

        [ReducerMethod]
        public static EditProductCategoryState OnEditProductCategory(EditProductCategoryState state, EditProductCategoryModifyProductCategoryAction action) => state with { Visibile = true, Saving = false, Model = action.Item };

        [ReducerMethod(typeof(EditProductCategorySaveProductCategoryAction))]
        public static EditProductCategoryState OnSaveProductCategory(EditProductCategoryState state) => state with { Saving = true };

        [ReducerMethod(typeof(EditProductCategoryEndSavingAction))]
        public static EditProductCategoryState OnEndSaving(EditProductCategoryState state) => state with { Saving = false };

        [ReducerMethod(typeof(EditProductCategoryEndEditProductCategoryAction))]
        public static EditProductCategoryState OnEndEditProductCategoryAction(EditProductCategoryState state) => state with { Visibile = false, Saving = false, Model = null };
    }

    #endregion

    #region Effects

    public class EditProductCategoryEffects
    {
        private readonly HomeManagementIndexedDb _homeManagementIndexedDb;

        public EditProductCategoryEffects(HomeManagementIndexedDb homeManagementIndexedDb)
        {
            _homeManagementIndexedDb = homeManagementIndexedDb;
        }

        [EffectMethod]
        public async Task SaveProductCategory(EditProductCategorySaveProductCategoryAction action, IDispatcher dispacher)
        {
            try
            {
                if (action.IsNew)
                {
                    var items = new List<ProductCategory>();
                    items.Add(new ProductCategory
                    {
                        Id = Guid.NewGuid().ToString(),
                        Description = action.Item.Description
                    });

                    await _homeManagementIndexedDb.AddNewItems<ProductCategory>(items).ConfigureAwait(false);
                }
                else
                {
                    var items = new List<ProductCategory>();
                    items.Add(action.Item);

                    await _homeManagementIndexedDb.EditItems<ProductCategory>(items).ConfigureAwait(false);
                }

                dispacher.Dispatch(new ProductCategoryLoadAction());
                dispacher.Dispatch(new EditProductCategoryEndEditProductCategoryAction());
            }
            catch (Exception ex)
            {
                dispacher.Dispatch(new EditProductCategoryEndSavingAction());
            }
        }
    }

    #endregion
}

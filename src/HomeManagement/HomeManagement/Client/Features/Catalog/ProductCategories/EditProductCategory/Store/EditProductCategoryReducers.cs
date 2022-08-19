using Fluxor;
using HomeManagement.Client.Features.Catalog.ProductCategories.Models;

namespace HomeManagement.Client.Features.Catalog.ProductCategories.EditProductCategory.Store
{
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
}

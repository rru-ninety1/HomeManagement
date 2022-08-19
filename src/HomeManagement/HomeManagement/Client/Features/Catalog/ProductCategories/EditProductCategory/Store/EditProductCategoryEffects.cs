using Fluxor;
using HomeManagement.Client.Features.Catalog.ProductCategories.Store;

namespace HomeManagement.Client.Features.Catalog.ProductCategories.EditProductCategory.Store
{
    public class EditProductCategoryEffects
    {
        [EffectMethod]
        public async Task SaveProductCategory(EditProductCategorySaveProductCategoryAction action, IDispatcher dispacher)
        {
            await Task.Delay(3000);
            //TODO salvare la categoria
            if (action.IsNew)
            {

            }
            else
            {

            }

            if (action.Item.Id == "1")
            {
                dispacher.Dispatch(new EditProductCategoryEndSavingAction());
            }
            else
            {
                dispacher.Dispatch(new ProductCategoryLoadAction());
                dispacher.Dispatch(new EditProductCategoryEndEditProductCategoryAction());
            }
        }
    }
}

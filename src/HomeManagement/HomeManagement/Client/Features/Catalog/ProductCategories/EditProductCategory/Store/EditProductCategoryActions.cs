using HomeManagement.Client.Features.Catalog.ProductCategories.Models;

namespace HomeManagement.Client.Features.Catalog.ProductCategories.EditProductCategory.Store
{
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
}

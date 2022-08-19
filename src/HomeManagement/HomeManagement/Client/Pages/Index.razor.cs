using Fluxor;
using HomeManagement.Client.Features.Catalog.ProductCategories.EditProductCategory.Store;
using Microsoft.AspNetCore.Components;

namespace HomeManagement.Client.Pages
{
    public partial class Index
    {
        [Inject]
        public IDispatcher Dispatcher { get; set; }


        public void EndEditCategory()
        {
            Dispatcher.Dispatch(new EditProductCategoryEndEditProductCategoryAction());
        }

        public void SaveCategory()
        {
            Dispatcher.Dispatch(new EditProductCategorySaveProductCategoryAction(new Features.Catalog.ProductCategories.Models.ProductCategory { Id = "1", Description = "descr1" }, false));
        }
    }
}

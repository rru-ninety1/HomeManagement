using Fluxor;
using HomeManagement.Client.Features.Catalog.ProductCategories.EditProductCategory.Store;
using HomeManagement.Client.Features.Catalog.ProductCategories.Models;
using HomeManagement.Client.Features.Catalog.ProductCategories.Store;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HomeManagement.Client.Features.Catalog.ProductCategories.Pages
{
    public partial class ProductCategoriesPage
    {
        [Inject]
        private IState<ProductCategoryState> ProductCategoryState { get; set; }

        [Inject]
        private IState<EditProductCategoryState> EditProductCategoryState { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        private bool Loading => ProductCategoryState.Value.Loading;

        private bool Initialized => ProductCategoryState.Value.Initialized;

        private bool OnEdit => EditProductCategoryState.Value.Visibile;

        private IEnumerable<ProductCategory> Categories => ProductCategoryState.Value.Categories;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (!ProductCategoryState.Value.Initialized)
            {
                Dispatcher.Dispatch(new ProductCategoryLoadAction());
            }
        }

        public void NewCategory()
        {
            Dispatcher.Dispatch(new EditProductCategoryAddNewProductCategoryAction());
        }

        public void EditCategory(ProductCategory productCategory)
        {
            Dispatcher.Dispatch(new EditProductCategoryModifyProductCategoryAction(productCategory));
        }

        public void RowClickEvent(TableRowClickEventArgs<ProductCategory> rowClickEvent)
        {
            Dispatcher.Dispatch(new EditProductCategoryModifyProductCategoryAction(rowClickEvent.Item));
        }
    }
}

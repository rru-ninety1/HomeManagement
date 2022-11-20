using Fluxor;
using HomeManagement.Client.Features.Catalog.ProductCategories.Models;
using HomeManagement.Client.Features.Catalog.ProductCategories.Store;
using HomeManagement.Client.Features.Catalog.Products.EditProduct.Store;
using HomeManagement.Client.Features.Catalog.Products.Models;
using Microsoft.AspNetCore.Components;

namespace HomeManagement.Client.Features.Catalog.Products.EditProduct.Components
{
    public partial class EditProductComponent : IDisposable
    {
        [Inject]
        private IState<EditProductState> State { get; set; }

        [Inject]
        private IState<ProductCategoryState> ProductCategoryStete { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        private bool Visible => State.Value.Visibile;
        private Product Model => State.Value.Model;
        private bool Saving => State.Value.Saving;

        private string _selectedCategoryId => State.Value.Model.CategoryId;

        private ProductCategory SelectedCategory
        {
            get
            {
                return _selectedCategoryId == null ? null : ProductCategoryStete.Value.Categories.First(x => x.Id == _selectedCategoryId);
            }
            set
            {
                State.Value.Model.CategoryId = value == null ? null : value.Id;
            }
        }

        private void Save()
        {
            bool isNew = Model.Id == null;
            Dispatcher.Dispatch(new EditProductSaveProductAction(Model, isNew));
        }

        private void Close()
        {
            Dispatcher.Dispatch(new EditProductEndEditProductAction());
        }

        private async Task<IEnumerable<ProductCategory>> SearchCategory(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return ProductCategoryStete.Value.Categories;
            }

            return ProductCategoryStete.Value.Categories.Where(x => x.Description.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}

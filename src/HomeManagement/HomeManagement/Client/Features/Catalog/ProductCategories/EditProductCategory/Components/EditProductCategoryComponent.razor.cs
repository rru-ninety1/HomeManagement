using Fluxor;
using HomeManagement.Client.Features.Catalog.ProductCategories.EditProductCategory.Store;
using HomeManagement.Client.Features.Catalog.ProductCategories.Models;
using Microsoft.AspNetCore.Components;

namespace HomeManagement.Client.Features.Catalog.ProductCategories.EditProductCategory.Components
{
    public partial class EditProductCategoryComponent : IDisposable
    {
        [Inject]
        private IState<EditProductCategoryState> State { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        private bool Visible => State.Value.Visibile;
        private ProductCategory Model => State.Value.Model;
        private bool Saving => State.Value.Saving;

        private void Save()
        {
            bool isNew = Model.Id == null;
            Dispatcher.Dispatch(new EditProductCategorySaveProductCategoryAction(Model, isNew));
        }
    }
}

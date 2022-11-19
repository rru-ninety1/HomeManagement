using Fluxor;
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
        public IDispatcher Dispatcher { get; set; }

        private bool Visible => State.Value.Visibile;
        private Product Model => State.Value.Model;
        private bool Saving => State.Value.Saving;

        private void Save()
        {
            bool isNew = Model.Id == null;
            Dispatcher.Dispatch(new EditProductSaveProductAction(Model, isNew));
        }

        private void Close()
        {
            Dispatcher.Dispatch(new EditProductEndEditProductAction());
        }
    }
}

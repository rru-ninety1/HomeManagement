using Fluxor;
using HomeManagement.Client.Features.Loader.Store;
using Microsoft.AspNetCore.Components;

namespace HomeManagement.Client
{
    public partial class App
    {
        [Inject]
        private IState<LoaderState> LoaderState { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        private bool Initialized => LoaderState.Value.Initialized;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Dispatcher.Dispatch(new LoaderStartLoadingAction());
        }
    }
}

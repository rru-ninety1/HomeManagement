using Fluxor;
using HomeManagement.Client.Features.CounterFeature.Store;
using Microsoft.AspNetCore.Components;

namespace HomeManagement.Client.Features.CounterFeature.Pages
{
    public partial class Counter
    {
        [Inject]
        private IState<CounterState> CounterState { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        private void IncrementCount()
        {
            var action = new IncrementCounterAction();
            Dispatcher.Dispatch(action);
        }
    }
}

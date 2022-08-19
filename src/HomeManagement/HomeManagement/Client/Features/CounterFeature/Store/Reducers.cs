using Fluxor;

namespace HomeManagement.Client.Features.CounterFeature.Store
{
    public static class Reducers
    {
        //[ReducerMethod]
        //public static CounterState ReduceIncrementCounterAction(CounterState state, IncrementCounterAction action) => state with { ClickCount = state.ClickCount + 1 };

        [ReducerMethod(typeof(IncrementCounterAction))]
        public static CounterState ReduceIncrementCounterAction(CounterState state) => state with { ClickCount = state.ClickCount + 1 };

    }
}

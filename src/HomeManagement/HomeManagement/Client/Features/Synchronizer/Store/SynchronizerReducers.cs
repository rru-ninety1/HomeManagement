using Fluxor;

namespace HomeManagement.Client.Features.Synchronizer.Store
{
    public static class SynchronizerReducers
    {
        [ReducerMethod(typeof(SynchronizerStartAction))]
        public static SynchronizerState OnStart(SynchronizerState state) => state with { OnSync = true };

        [ReducerMethod(typeof(SynchronizerEndAction))]
        public static SynchronizerState OnEnd(SynchronizerState state) => state with { OnSync = false, OperationDescription = "" };

        [ReducerMethod]
        public static SynchronizerState OnSetOperationDescription(SynchronizerState state, SynchronizerSetOperationDescriptionAction action) => state with { OperationDescription = action.OperationDescription };
    }
}

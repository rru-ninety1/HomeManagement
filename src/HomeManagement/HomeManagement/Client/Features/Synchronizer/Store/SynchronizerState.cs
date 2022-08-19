using Fluxor;

namespace HomeManagement.Client.Features.Synchronizer.Store
{
    [FeatureState(Name = "SynchronizerState")]
    public record class SynchronizerState
    {
        public bool OnSync { get; init; }
        public string OperationDescription { get; init; }

        public SynchronizerState() => (OnSync, OperationDescription) = (false, "");
    }
}

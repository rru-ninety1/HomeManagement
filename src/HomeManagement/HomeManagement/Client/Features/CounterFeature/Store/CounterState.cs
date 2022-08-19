using Fluxor;

namespace HomeManagement.Client.Features.CounterFeature.Store
{
    [FeatureState]
    public record class CounterState(int ClickCount)
    {
        public CounterState() : this(0) { }
    }
}

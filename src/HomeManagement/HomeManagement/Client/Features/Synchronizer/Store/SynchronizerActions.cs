namespace HomeManagement.Client.Features.Synchronizer.Store
{
    public class SynchronizerStartAction { }

    public class SynchronizerEndAction { }

    public class SynchronizerSetOperationDescriptionAction
    {
        public string OperationDescription { get; }

        public SynchronizerSetOperationDescriptionAction(string operationDescription)
        {
            OperationDescription = operationDescription;
        }
    }
}

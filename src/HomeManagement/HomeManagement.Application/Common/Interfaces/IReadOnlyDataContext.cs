namespace HomeManagement.Business.Common.Interfaces;

public interface IReadOnlyDataContext
{
    IQueryable<T> GetData<T>(bool trackingChanges = false, bool ignoreQueryFilters = false) where T : class;
}

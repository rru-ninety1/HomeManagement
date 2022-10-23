using DnetIndexedDb;
using Microsoft.JSInterop;

namespace HomeManagement.Client.Features.IndexedDB
{
    public class GridColumnDataIndexedDb : IndexedDbInterop
    {
        public GridColumnDataIndexedDb(IJSRuntime jsRuntime, IndexedDbOptions<GridColumnDataIndexedDb> options)
        : base(jsRuntime, options)
        {
        }
    }
}

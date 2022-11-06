using DnetIndexedDb;
using Microsoft.JSInterop;

namespace HomeManagement.Client.Features.Services.IndexedDB
{
    //https://github.com/amuste/DnetIndexedDb

    //GridColumnDataIndexedDb.DeleteAll("tableFieldDtos");

    //// Get by key
    //var result3 = await GridColumnDataIndexedDb.GetByKey<string, TableFieldDto>("tableFieldDtos", key1);

    //// Delete by key
    //var result4 = await GridColumnDataIndexedDb.DeleteByKey<string>("tableFieldDtos", key1);

    //var resultaa = await GridColumnDataIndexedDb.GetByIndex<string, TableFieldDto>("tableFieldDtos", "tipo2", null, "type", false);

    public class HomeManagementIndexedDb : IndexedDbInterop
    {
        private bool _opened = false;

        public HomeManagementIndexedDb(IJSRuntime jsRuntime, IndexedDbOptions<HomeManagementIndexedDb> options)
        : base(jsRuntime, options)
        {
            _opened = false;
        }

        public async Task<List<T>> GetAllItems<T>()
        {
            if (!_opened)
            {
                await OpenIndexedDb().ConfigureAwait(false);
                _opened = true;
            }

            return await GetAll<T>(typeof(T).Name).ConfigureAwait(false);
        }

        public async Task AddNewItems<T>(List<T> items)
        {
            if (!_opened)
            {
                await OpenIndexedDb().ConfigureAwait(false);
                _opened = true;
            }

            await AddItems(typeof(T).Name, items).ConfigureAwait(false);
        }

        public async Task EditItems<T>(List<T> items)
        {
            if (!_opened)
            {
                await OpenIndexedDb().ConfigureAwait(false);
                _opened = true;
            }

            await UpdateItems(typeof(T).Name, items).ConfigureAwait(false);
        }
    }
}

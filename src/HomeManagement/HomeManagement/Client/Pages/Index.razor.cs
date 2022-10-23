using HomeManagement.Client.Features.IndexedDB;
using Microsoft.AspNetCore.Components;

namespace HomeManagement.Client.Pages
{

    public partial class Index
    {
        [Inject]
        GridColumnDataIndexedDb GridColumnDataIndexedDb { get; set; }
        private async Task IncrementCount()
        {
            //https://github.com/amuste/DnetIndexedDb

            // Open and update indexeddb
            var result = await GridColumnDataIndexedDb.OpenIndexedDb();

            // delete all
            var resultDel = await GridColumnDataIndexedDb.DeleteAll("tableFieldDtos");

            string key1 = Guid.NewGuid().ToString();
            string key2 = Guid.NewGuid().ToString();
            string key3 = Guid.NewGuid().ToString();
            string key4 = Guid.NewGuid().ToString();

            var items = new List<TableFieldDto>();
            items.Add(new TableFieldDto
            {
                TableFieldId = key1,
                TableName = "Nome tab",
                FieldVisualName = "nome field",
                Type = "tipo"
            });
            items.Add(new TableFieldDto
            {
                TableFieldId = key2,
                TableName = "Nome tab2",
                Hide = false,
                Type = "tipo2"
            });
            items.Add(new TableFieldDto
            {
                TableFieldId = key3,
                TableName = "Nome tab3",
                Hide = false,
                Type = "tipo2"
            });
            items.Add(new TableFieldDto
            {
                TableFieldId = key4,
                TableName = "Nome tab4",
                Hide = false,
                Type = "tipo"
            });

            // Add to store
            var result2 = await GridColumnDataIndexedDb.AddItems<TableFieldDto>("tableFieldDtos", items);

            // Get by key
            var result3 = await GridColumnDataIndexedDb.GetByKey<string, TableFieldDto>("tableFieldDtos", key1);
            Console.WriteLine(result3.TableName);

            // Get all
            var resultAll = await GridColumnDataIndexedDb.GetAll<TableFieldDto>("tableFieldDtos");
            Console.WriteLine("N " + resultAll.Count + " " + resultAll[0].TableName + " " + resultAll[1].TableName);

            // Delete by key
            var result4 = await GridColumnDataIndexedDb.DeleteByKey<string>("tableFieldDtos", key1);

            resultAll.RemoveAll(x => x.TableFieldId == key1);
            resultAll[0].TableName += "aaaaa";
            var resultUpdate = await GridColumnDataIndexedDb.UpdateItems<TableFieldDto>("tableFieldDtos", resultAll);

            var resultaa = await GridColumnDataIndexedDb.GetByIndex<string, TableFieldDto>("tableFieldDtos", "tipo2", null, "type", false);
            Console.WriteLine("trovati " + resultaa.Count);
        }
    }

}

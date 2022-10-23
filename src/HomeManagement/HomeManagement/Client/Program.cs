using DnetIndexedDb;
using DnetIndexedDb.Fluent;
using DnetIndexedDb.Models;
using Fluxor;
using HomeManagement.Client;
using HomeManagement.Client.Features.IndexedDB;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

ConfigureServices(builder.Services);

await builder.Build().RunAsync();


void ConfigureServices(IServiceCollection services)
{
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


    services.AddFluxor(options =>
    {
        options.ScanAssemblies(typeof(Program).Assembly);
#if DEBUG
        options.UseReduxDevTools();
#endif
    });

    services.AddIndexedDbDatabase<GridColumnDataIndexedDb>(options =>
    {
        var model = new IndexedDbDatabaseModel()
            .WithName("TestDB")
            .WithVersion(1)
            .WithModelId(0);

        model.AddStore("tableFieldDtos")
            //.WithAutoIncrementingKey("tableFieldId")
            .WithKey("tableFieldId")
            .AddUniqueIndex("tableFieldId")
            .AddIndex("attachedProperty")
            .AddIndex("fieldVisualName")
            .AddIndex("hide")
            .AddIndex("isLink")
            .AddIndex("memberOf")
            .AddIndex("tableName")
            .AddIndex("textAlignClass")
            .AddIndex("type")
            .AddIndex("width");

        options.UseDatabase(model);
    });
}

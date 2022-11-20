using DnetIndexedDb;
using DnetIndexedDb.Fluent;
using DnetIndexedDb.Models;
using Fluxor;
using HomeManagement.Client;
using HomeManagement.Client.Features.Services.IndexedDB;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

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

    services.AddIndexedDbDatabase<HomeManagementIndexedDb>(options =>
    {
        var model = new IndexedDbDatabaseModel()
            .WithName("HomeManagementDB")
            .WithVersion(1)
            .WithModelId(0);

        model.AddStore("ProductCategory")
            .WithKey("Id")
            .AddUniqueIndex("Id")
            .AddIndex("Description");

        model.AddStore("Product")
            .WithKey("Id")
            .AddUniqueIndex("Id")
            .AddIndex("Description")
            .AddIndex("CategoryId");

        options.UseDatabase(model);
    });

    services.AddMudServices();
}

using Fluxor;
using HomeManagement.Client;
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
}

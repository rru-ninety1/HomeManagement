using HomeManagement.App.Services;
using HomeManagement.Business;
using HomeManagement.Business.Common.Interfaces;
using HomeManagement.Infrastructure;
using Microsoft.Extensions.Logging;

namespace HomeManagement.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            bool inMemoryDb = false;


            builder.Services.AddBusinessServices();
            builder.Services.AddInfrastructureServices(inMemoryDb, FileSystem.AppDataDirectory);
            builder.Services.AddSingleton<IDialogService, MauiDialogService>();

            var mauiApp = builder.Build();

            if (!inMemoryDb)
            {
                using (var scope = mauiApp.Services.CreateScope())
                {
                    var infrastructureBootstrapper = scope.ServiceProvider.GetRequiredService<Bootstraper>();
                    infrastructureBootstrapper.Execute();
                }
            }

            return mauiApp;
        }
    }
}
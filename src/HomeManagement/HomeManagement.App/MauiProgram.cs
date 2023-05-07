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

            builder.Services.AddBusinessServices();
            builder.Services.AddInfrastructureServices(true);
            builder.Services.AddSingleton<IDialogService, MauiDialogService>();

            return builder.Build();
        }
    }
}
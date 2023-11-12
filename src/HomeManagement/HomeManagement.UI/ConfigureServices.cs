using HomeManagement.Core.Localization;
using HomeManagement.UI.Features.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace HomeManagement.UI;
public static class ConfigureServices
{
    public static IServiceCollection AddAppUIServices(this IServiceCollection services)
    {
        services.AddLocalization();
        services.AddScoped<ILocalizationService, LocalizationService>();

        return services;
    }
}

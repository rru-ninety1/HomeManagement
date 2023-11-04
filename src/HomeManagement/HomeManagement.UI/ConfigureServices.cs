using Microsoft.Extensions.DependencyInjection;

namespace HomeManagement.UI;
public static class ConfigureServices
{
    public static IServiceCollection AddAppUIServices(this IServiceCollection services)
    {
        services.AddLocalization();

        return services;
    }
}

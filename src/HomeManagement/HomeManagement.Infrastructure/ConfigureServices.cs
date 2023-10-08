using HomeManagement.Business.Common.Interfaces;
using HomeManagement.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HomeManagement.Infrastructure;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, bool inMemory, string appDataDirectory)
    {
        if (inMemory)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("ApplicationDatabase");
            });
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                string dbFile = Path.Combine(appDataDirectory, "HomeManagement.db3");
                options.UseSqlite($"Data Source={dbFile}");
            });
        }

        services.AddScoped<IReadOnlyDataContext>(services => services.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IDataContext>(services => services.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<Bootstraper>();

        return services;
    }
}

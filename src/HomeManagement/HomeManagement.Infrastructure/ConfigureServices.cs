using HomeManagement.Business.Common.Interfaces;
using HomeManagement.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HomeManagement.Infrastructure;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, bool inMemory)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseInMemoryDatabase("ApplicationDatabase");
        });
        //services.AddDbContext<ApplicationDbContext>(options =>
        //   options.UseSqlServer(configuration.GetConnectionString("sql"),
        //       builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IReadOnlyDataContext>(services => services.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IDataContext>(services => services.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}

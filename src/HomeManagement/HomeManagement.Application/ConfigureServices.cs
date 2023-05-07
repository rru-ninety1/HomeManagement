using FluentValidation;
using HomeManagement.Business.Catalog.ProductCategories;
using HomeManagement.Business.Common.Behaviours;
using HomeManagement.Business.Common.CommandQuery;
using Microsoft.Extensions.DependencyInjection;

namespace HomeManagement.Business;

public static class ConfigureServices
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(ConfigureServices).Assembly);
            cfg.AddOpenBehavior(typeof(ExceptionBehavior<,>));
            cfg.AddOpenBehavior(typeof(ExceptionBehaviorT<,>));
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidationBehaviorT<,>));
        });

        services.AddScoped<IDispacher, MediatRDispacher>();

        services.AddValidatorsFromAssemblyContaining<ProductCategoryAddCommandValidator>();

        return services;
    }
}

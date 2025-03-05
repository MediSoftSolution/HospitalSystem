using System.Globalization;
using System.Reflection;
using FluentValidation;
using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Beheviors;
using HospitalSystem.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace HospitalSystem.Application;

public static class Registration
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddScoped<ExceptionMiddleware>();

        services.AddRulesFromAssemblyContaining(assembly, typeof(BaseRules));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        services.AddValidatorsFromAssembly(assembly);
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en");

        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehevior<,>));
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.Seq("http://localhost:5341")
            .CreateLogger();

    }
    private static IServiceCollection AddRulesFromAssemblyContaining(
        this IServiceCollection services,
        Assembly assembly,
        Type type)
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (var item in types)
            services.AddTransient(item);

        return services;    
    }
}
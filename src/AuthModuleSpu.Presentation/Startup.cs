using System.Text.Json;
using System.Text.Json.Serialization;
using AuthModuleSpu.Application;
using AuthModuleSpu.Infrastructure;
using AuthModuleSpu.Presentation.Configurations;
using Microsoft.AspNetCore.HttpLogging;
using Scalar.AspNetCore;

namespace AuthModuleSpu.Presentation;

public class Application
{
    private readonly WebApplication _app;
    
    public Application()
    {
        var builder = WebApplication.CreateBuilder();
        builder.Configuration.AddEnvironmentVariables();
        
        ConfigureServices(builder.Services, builder.Configuration);
        
        _app = builder.Build();
        ConfigureApp(_app);
    }
    
    public void Run() => _app.Run();

    private static void ConfigureApp(WebApplication app)
    {
        ConfigureExceptionHandler(app);
        ConfigureBasePath(app);
        ConfigureHttpLogger(app);
        ConfigureAuth(app);
        ConfigureRouting(app);
        ConfigureSwagger(app);
        app.MapControllers();
    }

    private static void ConfigureSwagger(WebApplication app)
    {
        app.MapOpenApi();
        app.MapScalarApiReference();
    }
    
    private static void ConfigureAuth(WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
    
    private static void ConfigureRouting(WebApplication app)
    {
        app.UseRouting();
        app.UseCors(
            cors => cors.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
        );
        app.MapControllers();
    }

    private static void ConfigureBasePath(WebApplication app)
    {
        var hostingConfiguration = app.Configuration
            .GetSection(nameof(HostingConfiguration))
            .Get<HostingConfiguration>();
        app.UsePathBase(hostingConfiguration!.BaseUrl);
    }
    
    private static void ConfigureHttpLogger(WebApplication app) => app.UseHttpLogging();
    
    private static void ConfigureExceptionHandler(WebApplication app)
    {
        // app.UseExceptionHandler();
        app.UseStatusCodePages();
    }
    
    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        ConfigureInternal(services, configuration);
        ConfigureExternal();
        ConfigureHttpLogger(services);
        ConfigureEndpoints(services);
        ConfigureMediatr(services);
        ConfigureSwagger(services);
    }

    private static void ConfigureInternal(IServiceCollection services, IConfiguration configuration)
    {
        InfrastructureInjection.Configure(services, configuration);
        ApplicationInjection.Configure(services, configuration);
    }

    private static void ConfigureExternal()
    {
        //TODO: Add your external services here
    }
    
    private static void ConfigureSwagger(IServiceCollection services)
    {
        services.AddOpenApi();
    }

    private static void ConfigureHttpLogger(IServiceCollection services) => 
        services.AddHttpLogging
        (
            options =>
            {
                options.LoggingFields = HttpLoggingFields.All;
                options.ResponseBodyLogLimit = 12 * 1024;
            }
        );

    private static void ConfigureEndpoints(IServiceCollection services) =>
        services.AddControllers()
            .AddJsonOptions
            (
                x =>
                {
                    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
                }
            );

    private static void ConfigureMediatr(IServiceCollection services) =>
        services.AddMediatR
        (
            serviceConfiguration =>
            {
                serviceConfiguration
                    .RegisterServicesFromAssemblies
                    (
                        AppDomain
                            .CurrentDomain
                            .GetAssemblies()
                    );
            }
        );
}
using Todo.Api.Infrastructure.Filters;
using Todo.App;
using Todo.Infrastructure;

namespace Todo.Api;

public static class ApiModule
{
    public static void ConfigureServices(IServiceCollection services)
    {
        InfrastructureModule.ConfigureServices(services);
        AppModule.ConfigureServices(services);
        services.AddControllers(options => options.Filters.Add<HttpGlobalExceptionFilter>());
    }
}

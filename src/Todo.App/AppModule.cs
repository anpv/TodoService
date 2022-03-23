using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Todo.App;

public static class AppModule
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddMediatR(typeof(AppModule));
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Domain.AggregatesModel;
using Todo.Infrastructure.Repositories;

namespace Todo.Infrastructure;

public static class InfrastructureModule
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<TodoContext>((serviceProvider, options) =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("Todo");
            options.UseSqlServer(connectionString);
        });
        services.AddScoped<ITodoItemRepository, TodoItemRepository>();
    }
}

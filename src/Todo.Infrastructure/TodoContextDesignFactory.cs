using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Todo.Infrastructure;

internal sealed class TodoContextDesignFactory : IDesignTimeDbContextFactory<TodoContext>
{
    public TodoContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TodoContext>()
            .UseSqlServer("Server=.;Database=Todo;Trusted_Connection=True");

        return new TodoContext(optionsBuilder.Options);
    }
}

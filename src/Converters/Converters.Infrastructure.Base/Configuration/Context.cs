using Converters.Infrastructure.Base.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Converters.Infrastructure.Base.Configuration;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConvertationConfiguration).Assembly);
    }
}
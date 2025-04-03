using System.Diagnostics.CodeAnalysis;
using Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data
{
    [ExcludeFromCodeCoverage]
    public class Context(DbContextOptions<Context> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PeopleConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

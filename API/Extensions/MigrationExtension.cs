using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Configurations
{
    public static class MigrationExtension
    {
        public static void ApplyMigration(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<Context>();
            context.Database.Migrate();
        }
    }
}

using Catstagram.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Runtime.CompilerServices;

namespace Catstagram.Server.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetRequiredService<CatstagramDbContext>();

            dbContext.Database.Migrate();
        }
    }
}

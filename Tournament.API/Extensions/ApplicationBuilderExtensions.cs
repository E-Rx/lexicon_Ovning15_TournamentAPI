using Microsoft.EntityFrameworkCore;
using Service.Contracts;
using Tournament.Data.Data;

namespace Tournament.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task SeedDataAsync(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var db = serviceProvider.GetRequiredService<TournamentAPIContext>();
                await db.Database.MigrateAsync();

                if (await db.TournamentDetails.AnyAsync())
                {
                    return;
                }

                try
                {
                    await Tournament.Data.Data.SeedData.InitAsync(db);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Bootcamp.Repository
{
    public static class SeedExt
    {
        public static void SeedDatabase(this WebApplication webApp)
        {
            using (var scope = webApp.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                SeedData.SeedDatabase(dbContext);
            }
        }
    }
}

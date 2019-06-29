using Eventures.Data;
using Eventures.Data.Seeding;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Eventures.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseDatabaseSeeding(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                Assembly.GetAssembly(typeof(EventuresDbContext))
                    .GetTypes()
                    .Where(type => typeof(ISeeder).IsAssignableFrom(type))
                    .Where(type => type.IsClass)
                    .Select(type => (ISeeder)serviceScope.ServiceProvider.GetRequiredService(type))
                    .ToList()
                    .ForEach(seeder => seeder.Seed());
            }
        }
    }
}

using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Eventures.Data.Seeding
{
    public class EventuresUserRoleSeeder : ISeeder
    {
        private readonly EventuresDbContext context;

        public EventuresUserRoleSeeder(EventuresDbContext context)
        {
            this.context = context;
        }

        public void Seed()
        {
            if (!this.context.Roles.Any())
            {
                this.context.Roles.Add(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
                this.context.Roles.Add(new IdentityRole { Name = "User", NormalizedName = "USER" });
            }

            this.context.SaveChanges();
        }
    }
}

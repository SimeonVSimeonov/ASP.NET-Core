using Eventures.Data.Entities.Enums;
using Microsoft.AspNetCore.Identity;

namespace Eventures.Data.Entities
{
    public class EventuresUser : IdentityUser
    {
        public EventuresUser()
        {
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UCN { get; set; }

        public UserRole Role { get; set; }
    }
}

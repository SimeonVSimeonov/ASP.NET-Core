using Eventures.Data.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace Eventures.Data.Entities
{
    public class EventuresUser : IdentityUser
    {
        public EventuresUser()
        {
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        [MaxLength(10)]
        public string UCN { get; set; }

        public UserRole Role { get; set; }
    }
}

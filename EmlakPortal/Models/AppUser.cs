using Microsoft.AspNetCore.Identity;

namespace EmlakPortal.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
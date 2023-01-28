using Microsoft.AspNetCore.Identity;

namespace BizLand.Models
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; }
    }
}

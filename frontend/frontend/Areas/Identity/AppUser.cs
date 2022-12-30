using Microsoft.AspNetCore.Identity;

namespace frontend.Areas.Identity
{
    public class AppUser : IdentityUser
    {
        public string ApplicationID { get; set; }
    }
}

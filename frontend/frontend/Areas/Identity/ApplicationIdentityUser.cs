using Microsoft.AspNetCore.Identity;

namespace frontend.Areas.Identity
{
    public class ApplicationIdentityUser : IdentityUser
    {
        public long ApplicationID { get; set; }
    }
}

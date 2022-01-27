using Microsoft.AspNetCore.Identity;

namespace E_Shopper_UI.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}

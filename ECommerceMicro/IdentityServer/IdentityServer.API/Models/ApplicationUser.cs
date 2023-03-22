using Microsoft.AspNetCore.Identity;

namespace IdentityServer.API.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }

    }
}

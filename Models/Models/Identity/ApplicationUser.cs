using Microsoft.AspNetCore.Identity;
namespace Models.Models.Identity
{
    public class ApplicationUser :IdentityUser
    {
        public string FirsName { get; set; }
        public string LastName { get; set; }
    }
}

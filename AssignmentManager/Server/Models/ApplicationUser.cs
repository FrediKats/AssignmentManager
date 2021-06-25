using Microsoft.AspNetCore.Identity;

namespace AssignmentManager.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int? IsuId { get; set; }
    }
}
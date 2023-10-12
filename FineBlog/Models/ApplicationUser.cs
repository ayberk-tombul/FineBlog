using Microsoft.AspNetCore.Identity;

namespace FineBlog.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set;}
        public List<Post>? Posts { get; set; }
    }
}

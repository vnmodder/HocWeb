using Microsoft.AspNetCore.Identity;

namespace HocWeb.Infrastructure.Entities
{
    public class User : IdentityUser<int>
    {
        public string? FullName { get; set; } = string.Empty;
    }
}

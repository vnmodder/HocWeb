using System;

namespace HocWeb.Infrastructure.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Type { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? AvatarUrl { get; set; }
    }
}

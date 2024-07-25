using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core
{
    public class AuthDTO
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? NewPassword { get; set; }
        public string? EMail { get; set; }
        public string? PhoneNumber { get; set; }
        public string? IpAddress { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
    }

    public record LoginDTO (string UserName, string Password);

    public record ChangePasswordDTO (string UserName, string Password, string NewPassword);

    public class AuthorizeDTO
    {
        public string Path { get; set; }
        public string Role { get; set; }
    }

    public record UserProfileDTO(string UserId);
}

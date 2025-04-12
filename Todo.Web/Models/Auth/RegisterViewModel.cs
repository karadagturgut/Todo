namespace Todo.Web.Models.Auth
{
    public class RegisterViewModel
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? NewPassword { get; set; }
        public string? EMail { get; set; }
        public string? PhoneNumber { get; set; }
        public string? IpAddress { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string OrganizationName { get; set; }
    }
}

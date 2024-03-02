namespace Common.ViewModel
{
    public class LoginViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Token { get; set; }
        public int SchoolId { get; set; }

        public DateTime Expiration { get; set; }
    }
}

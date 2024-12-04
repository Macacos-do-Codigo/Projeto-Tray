namespace Core.Models
{
    public class User
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        private User() { }

        public User(string? email, string? password)
        {
            Email = email;
            Password = password;
        }
    }
}

namespace Music.Models
{
    public class User
    {
        private readonly Login login; // Login can't be null
        private Password password;
        private string nickname = "User";
        private int? birthYear = null;

        private Login Login
        {
            init { this.login = value; }
        }

        private Password Password
        {
            set { this.password = value; }
        }

        // Register
        // Login
        // Logout
    }
}
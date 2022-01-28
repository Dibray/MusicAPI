namespace Music.Models
{
    public class Password
    {
        private string hash = null;

        private string Hash
        {
            get { return hash; }

            set
            {
                if (IsValidPassword(value))
                    this.hash = Encrypt(value);
                else
                {
                    if (Hash != null) // Hash exists, leave previous value
                        return;
                    else
                        throw new System.ArgumentException("Invalid Password");
                }
            }
        }

        Password(in string password)
        {
            Hash = password;
        }

        private static bool IsValidPassword(in string pass)
        {
            if (pass != null && pass.Length > 5)
                return true;

            return false;
        }

        private static string Encrypt(in string pass)
        {
            return System.Security.Cryptography.SHA256.Create(pass).ToString();
        }
    }
}

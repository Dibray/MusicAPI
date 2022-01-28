namespace Music.Models
{
    public class Password
    {
        private string hash = null;

        public string Hash
        {
            get { return hash; }

            private set
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

        public Password(in string password)
        {
            Hash = password;
        }

        public void ChangePassword(in string password)
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
            byte[] p = new byte[pass.Length];

            for (int i = 0; i < pass.Length; ++i) // Convert password to array of bytes
                p[i] = (byte)pass[i];

            byte[] h = System.Security.Cryptography.SHA256.Create().ComputeHash(p);

            string hash = "";

            for (int i = 0; i < h.Length; ++i) // Convert hash to string
                hash += (char)h[i];

            return hash;
        }
    }
}

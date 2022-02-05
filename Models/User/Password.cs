namespace Music.Models
{
    using hash_t = System.String;

    public class Password
    {
        internal class Db
        {
            private long Id { get; set; }

            private hash_t hash = null;
            /// <exception cref="System.ArgumentException"></exception>
            public hash_t Hash
            {
                get { return hash; }

                internal set
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

            public long UserId { get; private set; }
            public User.Db User { get; private set; }

            private Db() { }

            internal Db(in hash_t password)
            {
                Hash = password;
            }

            private static bool IsValidPassword(in hash_t pass)
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

        internal Db db { get; }

        public hash_t Hash
        { 
            get { return db.Hash; }

            set { this.db.Hash = value; }
        }

        public Password(in hash_t password)
        {
            db = new Db(password);
        }

        public void ChangePassword(in hash_t password)
        {
            Hash = password;
        }
    }
}

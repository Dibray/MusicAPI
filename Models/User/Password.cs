namespace Music.Models
{
    using hash_t = System.String;

    public class Password
    {
        internal class Db
        {
            private long Id { get; set; }
            private hash_t hash = null;

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

            private Db()
            {
                throw new System.InvalidOperationException("Password()_ctor");
            }

            internal Db(hash_t password)
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

        private Db DB;

        internal Db db
        {
            get { return DB; }
        }

        public hash_t Hash
        { 
            get { return DB.Hash; }

            set { this.DB.Hash = value; }
        }

        public Password(in hash_t password)
        {
            DB = new Db(password);
        }

        public void ChangePassword(in hash_t password)
        {
            Hash = password;
        }
    }
}

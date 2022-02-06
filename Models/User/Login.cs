namespace Music.Models
{
    using value_t = System.String;

    public class Login
    {
        public class Db
        {
            private readonly value_t value = null;
            /// <exception cref="System.ArgumentException"></exception>
            public value_t Value
            {
                get { return value; }

                private init
                {
                    if (IsValidValue(value))
                        this.value = value;
                    else
                        throw new System.ArgumentException("Invalid Login");
                }
            }

            public long UserId { get; private set; }
            public User.Db User { get; private set; }

            private Db() { }

            internal Db(in value_t value)
            {
                Value = value;
            }

            private static bool IsValidValue(in value_t value)
            {
                // True only if value has '@'

                if (value != null)
                    foreach (char c in value)
                        if (c == '@')
                            return true;

                return false;
            }
        }

        internal Db db { get; }

        public value_t Value
        {
            get { return db.Value; }
        }

        public Login(in value_t login)
        {
            db = new Db(login);
        }
    }
}

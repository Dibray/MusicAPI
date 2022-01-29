namespace Music.Models
{
    using value_t = System.String;

    public class Login
    {
        internal class Db
        {
            private readonly value_t value = null;

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

        private Db DB;

        internal Db db
        { 
            get { return DB; }
        }

        public value_t Value
        {
            get { return DB.Value; }
        }

        public Login(in value_t login)
        {
            DB = new Db(login);
        }
    }
}

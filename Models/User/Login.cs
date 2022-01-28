namespace Music.Models
{
    public class Login
    {
        private readonly string value = null;

        public string Value
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

        public Login(in string login)
        {
            Value = login;
        }

        private static bool IsValidValue(in string val)
        {
            // True only if value has '@'

            if (val != null)
                foreach (char c in val)
                    if (c == '@')
                        return true;

            return false;
        }
    }
}

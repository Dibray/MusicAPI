namespace Music.Models
{
    using firstName_t = System.String;
    using lastName_t = System.String;

    public class FullName
    {
        internal class Db
        {
            private long Id { get; set; }

            public firstName_t FirstName { get; internal set; } = "";

            public lastName_t LastName { get; internal set; } = "";

            public Author.Db Author { get; private set; }

            internal Db() { }
        }

        internal Db db { get; }

        public firstName_t FirstName
        {
            get { return this.db.FirstName; }

            private set { this.db.FirstName = value; }
        }

        public firstName_t LastName
        {
            get { return this.db.LastName; }

            private set { this.db.LastName = value; }
        }

        public string GetFullName
        {
            get { return FirstName + " " + LastName; }
        }

        public FullName()
        {
            db = new Db();
        }

        public FullName(in firstName_t firstName, in lastName_t lastName)
        {
            db = new Db();
            FirstName = firstName;
            LastName = lastName;
        }
    }
}

namespace Music.Models
{
    using System;
    using System.Threading.Tasks;

    public class Author
    {
        internal class Db
        {
            private long Id { get; set; }

            public FullName.Db FullName { get; internal set; } = new FullName().db;

            public DateTime BirthDate { get; internal set; }

            public long FullNameId { get; private set; }

            internal Db() { }
        }

        private Db db { get; }

        private FullName fullName = new FullName();
        private FullName FullName
        {
            set
            {
                this.fullName = value;
                this.db.FullName = value.db;
            }
        }

        private DateTime BirthDate
        {
            set { this.db.BirthDate = value; }
        }

        public Author()
        {
            db = new Db();
        }

        public Author(FullName fullName, DateTime birthDate)
        {
            db = new Db();
            FullName = fullName;
            BirthDate = birthDate;
        }

        /// <summary> Add author to database </summary>
        public static async Task<bool> NewAuthor(Author author)
        {
            Database.MusicContext db = new Database.MusicContext();

            await db.AddAsync(author.db);

            await db.SaveChangesAsync();

            return true;
        }
    }
}

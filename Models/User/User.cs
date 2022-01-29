namespace Music.Models
{
    using Music.Database;

    using System.Threading.Tasks;
    using System.Linq;

    using nickname_t = System.String;
    using birthYear_t = System.Nullable<int>;

    public class User
    {
        internal class Db
        {
            /* This class describes only database objects and must be handled only from outer class */

            private long Id { get; set; } // User Id
            private readonly Login.Db login;
            private Password.Db password;
            private nickname_t nickname = "User";
            private birthYear_t birthYear = null;
            public Role Role { get; private set; } = Role.User;

            public Login.Db Login
            {
                get { return login; }

                private init { this.login = value; }
            }

            public Password.Db Password
            {
                get { return password; }

                internal set { this.password = value; }
            }

            public nickname_t Nickname
            {
                get { return nickname; }

                internal set
                {
                    if (value != null)
                        this.nickname = value;
                }
            }

            public birthYear_t BirthYear
            {
                get { return birthYear; }

                internal set { this.birthYear = value; }
            }

            private Db()
            {
                // throw new System.InvalidOperationException("User.Db()_ctor");
            }

            internal Db(Login.Db loginDb, Password.Db passwordDb)
            {
                Login = loginDb;
                Password = passwordDb;
            }
        }

        private Db DB;

        private Db db
        {
            get { return DB; }
        }

        private readonly Login login;

        private Login Login
        {
            get { return login; }

            init { this.login = value; }
        }

        private Password password;

        private Password Password
        {
            set { this.password = value; }
        }

        private nickname_t Nickname
        {
            set { this.DB.Nickname = value; }
        }
    
        private birthYear_t BirthYear
        {
            set { this.DB.BirthYear = value; }
        }

        private User(Login login, Password password, nickname_t nickname, birthYear_t birthYear)
        {
            DB = new Db(login.db, password.db);
            Login = login;
            Nickname = nickname;
            BirthYear = birthYear;
        }

        /// <summary>
        /// Creates new user
        /// </summary>
        public static async Task<bool> Register(Login login, Password password, nickname_t nickname = null,
            birthYear_t birthYear = null)
        {
            MusicContext context = new MusicContext();

            if (context.Users.Any(u => u.Login.Value == login.Value)) // Check if login exists
                return false; // Registration unsuccessful

            await context.AddAsync(new User(login, password, nickname, birthYear).db);

            await context.SaveChangesAsync();

            return true; // Registration successful
        }

        // Login
        // Logout
    }
}
namespace Music.Models
{
    using Music.Database;

    using System.Threading.Tasks;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    using nickname_t = System.String;
    using birthYear_t = System.Nullable<int>;
    using authToken_t = System.String;

    public class User
    {
        internal class Db
        {
            /* This class describes only database objects and must be handled only from outer class */

            private long Id { get; set; } // User Id

            public Login.Db Login { get; private init; }

            public Password.Db Password { get; internal set; }

            public authToken_t AuthToken { get; internal set; } = null;

            public Role Role { get; private set; } = Role.User;

            public birthYear_t BirthYear { get; internal set; } = null;

            private nickname_t nickname = "User";
            public nickname_t Nickname
            {
                get { return nickname; }

                internal set
                {
                    if (value != null)
                        this.nickname = value;
                }
            }

            private Db() { }

            internal Db(in Login.Db loginDb, in Password.Db passwordDb)
            {
                Login = loginDb;
                Password = passwordDb;
            }
        }

        private Db db { get; }

        private Login Login { get; init; }

        private Password password;
        private Password Password
        {
            set { this.password = value; }
        }

        private nickname_t Nickname
        {
            set { this.db.Nickname = value; }
        }
    
        private birthYear_t BirthYear
        {
            set { this.db.BirthYear = value; }
        }

        private User(in Login login, in Password password, in nickname_t nickname, in birthYear_t birthYear)
        {
            db = new Db(login.db, password.db);
            Login = login;
            Nickname = nickname;
            BirthYear = birthYear;
        }

        /// <summary> Create new user </summary>
        /// <returns> Registration success </returns>
        public static async Task<bool> Register(Login login, Password password, nickname_t nickname = null,
            birthYear_t birthYear = null)
        {
            MusicContext db = new MusicContext();

            if (db.Users.Any(u => u.Login.Value == login.Value)) // Check if login exists
                return false; // Registration unsuccessful

            await db.AddAsync(new User(login, password, nickname, birthYear).db);

            await db.SaveChangesAsync();

            return true; // Registration successful
        }

        /// <summary> Log in user and issue authorization token </summary>
        /// <returns> Authorization token </returns>
        public static async Task<authToken_t> LogIn(Login login, Password password)
        {
            MusicContext db = new MusicContext();

            Db user = db.Users
                .Include(u => u.Login).Include(u => u.Password).FirstOrDefault(u => u.Login.Value == login.Value);

            if (user == null || password.Hash != user.Password.Hash)
                return null; // User with specified login doesn't exists or password is incorrect

            // Create authorization token based on login, password and current time
            user.AuthToken = new Password(user.Login.Value + System.DateTime.Now.ToString() + user.Password.Hash).Hash;

            await db.SaveChangesAsync();

            return user.AuthToken;
        }

        // Logout
    }
}
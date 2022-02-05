namespace Models.Testing
{
    using Xunit;

    using Music.Models;

    public class UserTest
    {
        [Fact]
        private static async void Login_Password()
        {
            /* Registration() true */

            Login login = new Login("@" + Time());
            Password password = new Password(Time());

            Assert.True(await User.Register(login, password));
        }

        [Fact]
        private async static void Login_Password_Nickname()
        {
            /* Registration() true */

            Login login = new Login("@" + Time());
            Password password = new Password(Time());

            Assert.True(await User.Register(login, password, "d32ddf3"));
        }

        [Fact]
        private async static void Login_Password_BirthYear()
        {
            /* Registration() true */

            Login login = new Login("@" + Time());
            Password password = new Password(Time());

            Assert.True(await User.Register(login, password, null, 1994));
        }

        [Fact]
        private async static void SamePassword()
        {
            /* Registration() true */

            Login login = new Login("@" + Time());
            Password password = new Password("123456");

            Assert.True(await User.Register(login, password));
        }

        [Fact]
        private async static void SameLogin()
        {
            /* Registration() false */

            Login login = new Login("@Test");
            Password password = new Password("123456");

            Assert.False(await User.Register(login, password));
        }

        [Fact]
        private async static void LogInExistingUser()
        {
            /* LogIn() true */

            Login login = new Login("admin@music.com");
            Password password = new Password("123456");

            Assert.NotNull(await User.LogIn(login, password));
        }

        [Fact]
        private async static void LogInUnexistingUser()
        {
            /* LogIn() false */

            Login login = new Login("2222212@");
            Password password = new Password("123456");

            Assert.Null(await User.LogIn(login, password));
        }

        private static string Time()
        {
            return System.DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
        }
    }
}

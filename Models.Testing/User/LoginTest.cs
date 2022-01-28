namespace Models.Testing
{
    using Xunit;

    using Music.Models;

    public class LoginTest
    {
        [Fact]
        private void Valid1()
        {
            Login login = new Login("adwad@we");

            Assert.Equal("adwad@we", login.Value);
        }

        [Fact]
        private void Invalid1()
        {
            Assert.Throws<System.ArgumentException>(() => new Login("vls\\kjfh"));
        }

        [Fact]
        private void Null()
        {
            Assert.Throws<System.ArgumentException>(() => new Login(null));
        }

        [Fact]
        private void Empty()
        {
            Assert.Throws<System.ArgumentException>(() => new Login(""));
        }
    }
}

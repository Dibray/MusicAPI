namespace Models.Testing
{
    using Xunit;
    using System;

    using Music.Models;

    public class PasswordTest
    {
        [Fact]
        private void NullPassword()
        {
            /* IsValidPassword() false */

            Assert.Throws<ArgumentException>(() => new Password(null));
        }

        [Fact]
        private void PasswordLength_s5()
        {
            /* IsValidPassword() false */

            Assert.Throws<ArgumentException>(() => new Password(""));
        }

        [Fact]
        private void PasswordLength_s5_1()
        {
            /* IsValidPassword() false */

            Assert.Throws<ArgumentException>(() => new Password("231"));
        }

        [Fact]
        private void PasswordLength_e5()
        {
            /* IsValidPassword() false */

            Assert.Throws<ArgumentException>(() => new Password("jdyu7"));
        }

        [Fact]
        private void PasswordLength_g5()
        {
            /* IsValidPassword() true */

            Password pass = new Password("s3jd9v");

            Assert.NotNull(pass.Hash);
        }

        [Fact]
        private void PasswordLength_s5_NotNullHash()
        {
            /* Hash_set return */

            Password pass = new Password("e3233d");

            var oldHash = pass.Hash;

            pass.ChangePassword("e31t");

            Assert.Equal(pass.Hash, oldHash);
        }
    }
}

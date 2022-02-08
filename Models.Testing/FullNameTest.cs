namespace Models.Testing
{
    using Xunit;

    using Music.Models;

    public class FullNameTest
    {
        [Fact]
        private static void FirstName_LastName()
        {
            FullName name = new FullName("F", "L");

            Assert.Equal("FL", name.FirstName + name.LastName);
        }

        [Fact]
        private static void FullName()
        {
            FullName name = new FullName("F", "L");

            Assert.Equal("F L", name.GetFullName);
        }
    }
}

using shopping_website;
using Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ShoppingWebsite.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void CreateHostBuilder_ReturnsHostBuilder()
        {
            var builder = Program.CreateHostBuilder(new string[] { });
            Assert.NotNull(builder);
            Assert.IsAssignableFrom<IHostBuilder>(builder);
        }
    }

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }
    }
}
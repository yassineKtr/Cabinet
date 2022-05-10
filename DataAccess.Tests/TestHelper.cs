using Microsoft.Extensions.Configuration;

namespace DataAccess.Tests
{
    public class TestHelper
    {
        public static IConfiguration GetIConfigurationRoot() =>
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
    }
}

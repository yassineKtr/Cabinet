using DataAccess.DbAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DataAccess.Tests
{
    public class TestHelper
    {
        public static IConfiguration config;
        private static PostgresqlConfig postgresqlConfig;
        public static IOptions<PostgresqlConfig> configuration;
        public static IPostgresqlConnection GetConnection()
        {
            config =  new ConfigurationBuilder()
                        .AddJsonFile("appsettings.test.json")
                        .Build();
            postgresqlConfig = new PostgresqlConfig();
            configuration = config.GetSection("Postgresql").Bind(postgresqlConfig);
            return  new PostgresqlConnection(configuration);
        }
    }
}
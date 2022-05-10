using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DataAccess.DbAccess
{
    public class PostgresqlConnection : IPostgresqlConnection
    {
        private readonly NpgsqlConnectionStringBuilder _connectionStringBuilder;

        public PostgresqlConnection(IConfiguration config)
        {
            var configuration = new PostgresqlConfig()
            {
                Host = config.GetSection("ConnectionParams")["host"],
                Port = Int32.Parse(config.GetSection("ConnectionParams")["port"]),
                UserName = config.GetSection("ConnectionParams")["username"],
                DataBase = config.GetSection("ConnectionParams")["database"],
                Password = config.GetSection("ConnectionParams")["password"],
            };
            _connectionStringBuilder = new NpgsqlConnectionStringBuilder
            {
                Host = configuration.Host,
                Port = configuration.Port,
                Username = configuration.UserName,
                Database = configuration.DataBase,
                Password = configuration.Password
            };
        }
        public NpgsqlConnection GetSqlConnection() => new(_connectionStringBuilder.ConnectionString);
    }
}

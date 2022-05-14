using Microsoft.Extensions.Options;
using Npgsql;

namespace DataAccess.DbAccess
{
    public class PostgresqlConnection : IPostgresqlConnection
    {
        private readonly NpgsqlConnectionStringBuilder _connectionStringBuilder;

        public PostgresqlConnection(IOptions<PostgresqlConfig> opt)
        {
            _connectionStringBuilder = new NpgsqlConnectionStringBuilder
            {
                Host = opt.Value.Host,
                Port = opt.Value.Port,
                Username = opt.Value.UserName,
                Database = opt.Value.DataBase,
                Password = opt.Value.Password
            };
        }
        public NpgsqlConnection GetSqlConnection() => new(_connectionStringBuilder.ConnectionString);
    }
}

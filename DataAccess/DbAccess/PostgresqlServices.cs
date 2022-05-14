using Dapper;

namespace DataAccess.DbAccess
{
    public class PostgresqlServices : IPostgresqlServices
    {
        private readonly IPostgresqlConnection _connection;
        public PostgresqlServices(IPostgresqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<T>?> QueryDb<T>(string query, Object param)
        {
            await using var connection = _connection.GetSqlConnection();
            await connection.OpenAsync();
            return await connection.QueryAsync<T>(query, param);
        }

        public async Task Execute(string query, Object param)
        {
            await using var connection = _connection.GetSqlConnection();
            await connection.OpenAsync();
            await connection.ExecuteAsync(query, param);
        }
    }
}

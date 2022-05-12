using Dapper;
using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Readers.Clients
{
    public class ClientReader : IReadClient
    {
        private readonly IPostgresqlConnection _connection;
        public ClientReader(IPostgresqlConnection connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<Client>> GetAllClients()
        {
            var query = "SELECT * FROM client";
            await using var connection = _connection.GetSqlConnection();
            await connection.OpenAsync();
            return await connection.QueryAsync<Client>(query, new { });
        }
        public async Task<Client?> GetClientById(Guid id)
        {
            var query = "SELECT * FROM client WHERE client_id = @id";
            await using var connection = _connection.GetSqlConnection();
            await connection.OpenAsync();
            return await connection.QueryFirstOrDefaultAsync<Client>(query, new { id });
        }
        
    }
}

using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Readers.Clients
{
    public class ClientReader : IReadClient
    {
        private readonly IPostgresqlServices _connection;
        public ClientReader(IPostgresqlServices connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<Client>?> GetAllClients()
        {
            var query = $"SELECT * FROM {DbTables.client}";
            return await _connection.QueryDb<Client>(query, new { });
        }
        public async Task<Client?> GetClientById(Guid id)
        {
            var query = $"SELECT * FROM {DbTables.client} WHERE client_id = @id";
            var result =  await _connection.QueryDb<Client>(query, new { id });
            return result?.FirstOrDefault();
        }
        
    }
}

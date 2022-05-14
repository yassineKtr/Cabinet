using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Writers.Clients
{
    public class ClientWriter : IWriteClient
    {
        private readonly IPostgresqlServices _connection;
        public ClientWriter(IPostgresqlServices connection)
        {
            _connection = connection;
        }
        public async Task AddClient(Client client)
        {
            var query = $"INSERT INTO {DbTables.client} (client_id, nom, prenom, email, telephone) " +
                        "VALUES (@id, @nom, @prenom, @email, @telephone)";
            var parameters = new 
            {
                id=client.Client_id,
                nom= client.Nom,
                prenom= client.Prenom,
                email= client.Email,
                telephone= client.Telephone
            };
            await _connection.Execute(query, parameters);
        }
        public async Task UpdateClient(Client client)
        {
            var query = $"UPDATE {DbTables.client} SET nom = @nom, prenom = @prenom, email = @email, telephone = @telephone " +
                        "WHERE client_id = @id";
            var parameters = new
            {
                id = client.Client_id,
                nom = client.Nom,
                prenom = client.Prenom,
                email = client.Email,
                telephone = client.Telephone
            };
            await _connection.Execute(query, parameters);
        }
        public async Task DeleteClient(Guid client_id)
        {
            var query = $"DELETE FROM {DbTables.client} WHERE client_id = @id";
            var parameters = new
            {
                id = client_id
            };
            await _connection.Execute(query, parameters);
        }
    }
}

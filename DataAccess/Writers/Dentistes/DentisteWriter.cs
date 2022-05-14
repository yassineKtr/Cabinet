using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Writers.Dentistes
{
    public class DentisteWriter : IWriteDentiste
    {
        private readonly IPostgresqlServices _connection;
        public DentisteWriter(IPostgresqlServices connection)
        {
            _connection = connection;
        }
        public async Task AddDentiste(Dentiste dentiste)
        {
            var query = $"INSERT INTO {DbTables.dentiste} (dentiste_id, nom, prenom, debut_travail, fin_travail, max_clients)" +
                        " VALUES (@id, @nom, @prenom, @debut_travail, @fin_travail, @max_clients)";
            var parameters = new
            {
                id = dentiste.Dentiste_id,
                nom = dentiste.Nom,
                prenom = dentiste.Prenom,
                debut_travail = dentiste.Debut_travail,
                fin_travail = dentiste.Fin_travail,
                max_clients = dentiste.Max_clients
            };
            await _connection.Execute(query, parameters);
        }
        public async Task UpdateDentiste(Dentiste dentiste)
        {
            var query = $"UPDATE {DbTables.dentiste} " +
                        "SET nom = @nom, prenom = @prenom, debut_travail = @debut_travail, fin_travail = @fin_travail, max_clients = @max_clients" +
                        " WHERE dentiste_id = @id";
            var parameters = new
            {
                id = dentiste.Dentiste_id,
                nom = dentiste.Nom,
                prenom = dentiste.Prenom,
                debut_travail = dentiste.Debut_travail,
                fin_travail = dentiste.Fin_travail,
                max_clients = dentiste.Max_clients
            };
            await _connection.Execute(query, parameters);
        }
        public async Task DeleteDentiste(Guid id)
        {
            var query = $"DELETE FROM {DbTables.dentiste} WHERE dentiste_id = @id";
            var parameters = new { id = id };
            await _connection.Execute(query, parameters);
        }
    }
}

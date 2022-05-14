using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Readers.RendezVouss
{
    public class RendezVousReader : IReadRendezVous
    {
        private readonly IPostgresqlServices _connection;
        public RendezVousReader(IPostgresqlServices connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<RendezVous>?> GetAllRendezVous()
        {
            var sql = $"SELECT * FROM {DbTables.rendezvous}";
            return await _connection.QueryDb<RendezVous>(sql,new{});
        }
        public async Task<RendezVous?> GetRendezVousById(Guid id)
        {
            var sql = $"SELECT * FROM {DbTables.rendezvous} WHERE rdv_id = @id";
            var result =  await _connection.QueryDb<RendezVous>(sql, new { id });
            return result?.FirstOrDefault();
        }

        public async Task<IEnumerable<RendezVous>?> GetRendezVousByDate(DateTime date)
        {
            var sql = $"SELECT * FROM {DbTables.rendezvous} WHERE date_rdv = @date";
            return await _connection.QueryDb<RendezVous>(sql, new { date });
        }

        public async Task<IEnumerable<RendezVous>?> GetRendezVousByClientId(Guid id)
        {
            var sql = $"SELECT * FROM {DbTables.rendezvous} WHERE client_id = @id";
            return await _connection.QueryDb<RendezVous>(sql, new {id });
        }

        public async Task<IEnumerable<RendezVous>?> GetRendezVousByDentisteId(Guid id)
        {
            var sql = $"SELECT * FROM {DbTables.rendezvous} WHERE dentiste_id = @id";
            return await _connection.QueryDb<RendezVous>(sql, new { id });
        }
        public async Task<IEnumerable<RendezVous>?> GetRendezVousByConsultationId(Guid id)
        {
            var sql = $"SELECT * FROM {DbTables.rendezvous} WHERE consultation_id = @id";
            return await _connection.QueryDb<RendezVous>(sql, new { id });
        }
    }
}

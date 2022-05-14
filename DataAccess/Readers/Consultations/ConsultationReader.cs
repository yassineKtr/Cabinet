using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Readers.Consultations
{
    public class ConsultationReader : IReadConsultation
    {
        private readonly IPostgresqlServices _connection;
        public ConsultationReader(IPostgresqlServices connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<Consultation>?> GetConsultations()
        {
            var sql = $"SELECT * FROM {DbTables.consultation}";
            return await _connection.QueryDb<Consultation>(sql,new{});           
        }
        public async Task<Consultation?> GetConsultationById(Guid id)
        {
            var sql = $"SELECT * FROM {DbTables.consultation} WHERE consultation_id = @id";
            var result = await _connection.QueryDb<Consultation>(sql, new { id });
            return result?.FirstOrDefault();
        }
        public async Task<Consultation?> GetConsultationByType(string type)
        {
            var sql = $"SELECT * FROM {DbTables.consultation} WHERE consultation_type = @type";
            var result = await _connection.QueryDb<Consultation>(sql, new { type });
            return result?.FirstOrDefault();
        }

    }
}

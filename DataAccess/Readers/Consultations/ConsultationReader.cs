using Dapper;
using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Readers.Consultations
{
    public class ConsultationReader : IReadConsultation
    {
        private readonly IPostgresqlConnection _connection;
        public ConsultationReader(IPostgresqlConnection connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<Consultation>> GetConsultations()
        {
            var sql = "SELECT * FROM consultation";
            await using var connection = _connection.GetSqlConnection();
            await connection.OpenAsync();
            return await connection.QueryAsync<Consultation>(sql,new{});
            
        }
        public async Task<Consultation?> GetConsultationById(Guid id)
        {
            var sql = "SELECT * FROM consultation WHERE consultation_id = @id";
            await using var connection = _connection.GetSqlConnection();
            await connection.OpenAsync();
            return await connection.QueryFirstOrDefaultAsync<Consultation>(sql, new { id });
        }
        public async Task<Consultation?> GetConsultationByType(string type)
        {
            var sql = "SELECT * FROM consultation WHERE consultation_type = @type";
            await using var connection = _connection.GetSqlConnection();
            await connection.OpenAsync();
            return await connection.QueryFirstOrDefaultAsync<Consultation>(sql, new { type });
        }

    }
}

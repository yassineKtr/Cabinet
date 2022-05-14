using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Writers.Consultations
{
    public class ConsultationWriter : IWriteConsultation
    {
        private readonly IPostgresqlServices _connection;
        public ConsultationWriter(IPostgresqlServices connection)
        {
            _connection = connection;
        }
        public async Task AddConsultation(Consultation consultation)
        {
            var query = $"INSERT INTO {DbTables.consultation} (consultation_id,consultation_type,prix) " +
                        "VALUES (@consultation_id,@consultation_type,@prix)";
            var parameters = new
            {
                consultation_id = consultation.Consultation_id,
                consultation_type = consultation.Consultation_type,
                prix = consultation.Prix
            };
            await _connection.Execute(query, parameters);
        }
        public async Task UpdateConsultation(Consultation consultation)
        {
            var query = $"UPDATE {DbTables.consultation} SET consultation_type = @consultation_type, prix = @prix " +
                        "WHERE consultation_id = @consultation_id";
            var parameters = new
            {
                consultation_id = consultation.Consultation_id,
                consultation_type = consultation.Consultation_type,
                prix = consultation.Prix
            };
            await _connection.Execute(query, parameters);
        }
        public async Task DeleteConsultation(Guid consultation_id)
        {
            var query = $"DELETE FROM {DbTables.consultation} WHERE consultation_id = @consultation_id";
            var parameters = new { consultation_id };
            await _connection.Execute(query, parameters);
        }
    }
}

using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Readers.Dentists
{
    public class DentisteReader : IReadDentiste
    {
        private readonly IPostgresqlServices _connection;
        public DentisteReader(IPostgresqlServices connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<Dentiste>?> GetDentistes()
        {
            var sql = $"SELECT * FROM {DbTables.dentiste}";
            return await _connection.QueryDb<Dentiste>(sql, new { });
        }
        public async Task<Dentiste?> GetDentisteById(Guid id)
        {
            var sql = $"SELECT * FROM {DbTables.dentiste} WHERE dentiste_id = @id";
            var result = await _connection.QueryDb<Dentiste?>(sql, new { id });
            return result?.FirstOrDefault();
        }

        public async Task<Dentiste?> GetDentisteByName(string name)
        {
            var sql = $"SELECT * FROM {DbTables.dentiste} WHERE nom=@name";
            var result = await _connection.QueryDb<Dentiste?>(sql, new { name });
            return result?.FirstOrDefault();
        }
    }
}

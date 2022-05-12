using Dapper;
using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Readers.Dentists
{
    public class DentisteReader : IReadDentiste
    {
        private readonly IPostgresqlConnection _connection;
        public DentisteReader(IPostgresqlConnection connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<Dentiste>> GetDentistes()
        {
            var sql = "SELECT * FROM dentiste";
            await using var connection = _connection.GetSqlConnection();
            await connection.OpenAsync();
            return await connection.QueryAsync<Dentiste>(sql, new { });
        }
        public async Task<Dentiste?> GetDentisteById(Guid id)
        {
            var sql = "SELECT * FROM dentiste WHERE dentiste_id = @id";
            await using var connection = _connection.GetSqlConnection();
            await connection.OpenAsync();
            return await connection.QueryFirstOrDefaultAsync<Dentiste?>(sql, new { id });
        }

        public async Task<Dentiste?> GetDentisteByName(string name)
        {
            var sql = "SELECT * FROM dentiste WHERE nom=@name";
            await using var connection = _connection.GetSqlConnection();
            await connection.OpenAsync();
            var dentiste = await connection.QueryFirstOrDefaultAsync<Dentiste>(sql, new { name });
            return dentiste;
        }
    }
}

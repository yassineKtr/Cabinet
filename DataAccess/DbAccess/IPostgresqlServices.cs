
namespace DataAccess.DbAccess
{
    public interface IPostgresqlServices
    {
        Task Execute(string query, object param);
        Task<IEnumerable<T>?> QueryDb<T>(string query, object param);
    }
}
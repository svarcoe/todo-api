using System.Data.SqlClient;

namespace todo_api.Todos
{
    public interface ISqlConnectionFactory
    {
        SqlConnection GetConnection();
    }
}
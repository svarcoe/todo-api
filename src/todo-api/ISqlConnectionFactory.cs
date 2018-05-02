using System.Data.SqlClient;

namespace Todo.Api
{
    public interface ISqlConnectionFactory
    {
        SqlConnection GetConnection();
    }
}
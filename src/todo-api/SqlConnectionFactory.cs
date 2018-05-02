using System.Data.SqlClient;
using Microsoft.Extensions.Options;
using todo_api.Todos;

namespace todo_api
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly DatabaseOptions _databaseOptions;

        public SqlConnectionFactory(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions.Value;
        }
        public SqlConnection GetConnection()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = _databaseOptions.Server;
            builder.UserID = _databaseOptions.UserId;
            builder.Password = _databaseOptions.Password;

            if (!string.IsNullOrEmpty(_databaseOptions.Name))
            {
                builder.InitialCatalog = _databaseOptions.Name;
            }

            return new SqlConnection(builder.ToString());
        }
    }
}
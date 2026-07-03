namespace Irt.Infrastructure.Database
{
    using System.Data;
    using System.Data.SqlClient;
    using Irt.Application.Configuration.Data;

    public class SqlConnectionFactory(string connectionString) : ISqlConnectionFactory, IDisposable
    {
        private IDbConnection? _connection;

        public void Dispose()
        {
            if (_connection?.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        public IDbConnection GetOpenConnection()
        {
            if (_connection is null || _connection.State != ConnectionState.Open)
            {
                _connection = new SqlConnection(connectionString);
                _connection.Open();
            }

            return _connection;
        }
    }
}

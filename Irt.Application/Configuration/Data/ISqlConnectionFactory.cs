using System.Data;

namespace Irt.Application.Configuration.Data
{

    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}
using System.Data;
using System.Threading.Tasks;

namespace Nanasaki.Data;

public interface IDbConnectionFactory
{
    public Task<IDbConnection> CreateConnectionAsync();
}
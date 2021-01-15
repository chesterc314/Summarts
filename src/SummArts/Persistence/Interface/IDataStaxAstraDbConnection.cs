using Cassandra;

namespace Persistence.Interface
{
    public interface IDataStaxAstraDbConnection
    {
        ISession Session { get; }
    }
}

using Cassandra;
using Microsoft.Extensions.Configuration;
using Persistence.Interface;

namespace SummArts.Persistence
{
    public class DataStaxAstraDbConnection : IDataStaxAstraDbConnection
    {
        private readonly IConfiguration _configuration;
        public DataStaxAstraDbConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ISession Session
        {
            get
            {
                var cluster = Cluster.Builder()
                          .WithCloudSecureConnectionBundle(_configuration["BundlePath"])
                          .WithCredentials(_configuration["AstraUsername"], _configuration["AstraPassword"])
                          .Build();
                return cluster.Connect(_configuration["KeySpace"]);
            }
        }
    }
}
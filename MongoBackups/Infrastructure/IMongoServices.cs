using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoBackups.Model;

namespace MongoBackups.Infrastructure
{
    public interface IMongoServices
    {
        Task<List<Collection>> toListCollectionMongoDB(string serverName);
        Task<bool> Verifyconnection(string connectionString);
    }
}

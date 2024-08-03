using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoBackups.Infrastructure
{
    public interface IExecMongoCommand
    {
        bool mongoDump(string _database, string _connectionString, string _path);
        bool mongoExport(string _database, string _collection, string _connectionString, string _path);
        bool mongoImport(string _database, string _collection, string _connectionString, string _path);
    }
}

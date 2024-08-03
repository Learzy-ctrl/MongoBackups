using MongoDB.Driver;
using MongoDB.Bson;
using MongoBackups.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoBackups.Model;

namespace MongoBackups.Services
{
    public class MongoServices : IMongoServices
    {
        public async Task<List<Collection>> toListCollectionMongoDB(string serverName)
        {
            var ListCollection = new List<Collection>();
            string connection = $"mongodb://{serverName}";
            var client = new MongoClient(connection);
            var databaseList = await client.ListDatabaseNamesAsync();
            foreach (var database in databaseList.ToList())
            {
                var db = client.GetDatabase(database);
                var listCollect = await db.ListCollectionNamesAsync();
                foreach (var coll in listCollect.ToList())
                {
                    var oCollection = new Collection();
                    var collection = db.GetCollection<BsonDocument>(coll);

                    oCollection.DatabaseName = database;
                    oCollection.Name = coll;
                    oCollection.Documents = await collection.EstimatedDocumentCountAsync();

                    ListCollection.Add(oCollection);
                }

            }
            return ListCollection;
        }

        public async Task<bool> Verifyconnection(string _connectionString)
        {
            string connectionString = $"mongodb://{_connectionString}";
            try
            {
                var mongoClient = new MongoClient(connectionString);

                var response = await mongoClient.ListDatabaseNamesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

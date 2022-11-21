using MongoDB.Driver;


namespace NLayerEmarket.Persistence.Collections
{
    public sealed class MongoDbManager
    {
        public static IMongoDatabase GetDatabase(IMongoClient client)
        {
            return client.GetDatabase("emarket");
        }
    }
}

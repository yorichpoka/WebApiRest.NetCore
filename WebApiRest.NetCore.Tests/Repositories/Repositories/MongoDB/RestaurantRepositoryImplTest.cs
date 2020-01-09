using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using WebApiRest.NetCore.Repositories.Entities.MongoDB;
using Xunit;

namespace WebApiRest.NetCore.Tests.Repositories.Repositories.MongoDB
{
    public class RestaurantRepositoryImplTest
    {
        [Theory]
        [InlineData("mongodb://vmsqldev01:27017/", "restaurants", "restaurants")]
        public void GetTest(string connectionString, string databaseName, string collectionName)
        {
            IMongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase(databaseName);
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collectionName);

            BsonDocument filter = new BsonDocument();

            using (var cursor = collection.FindAsync(filter).Result)
            {
                while (cursor.MoveNextAsync().Result)
                {
                    foreach (BsonDocument document in cursor.Current)
                    {
                        var myObj = BsonSerializer.Deserialize<Restaurant>(document);
                    }
                }
            }
        }

        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        [Fact]
        public void FailingTest()
        {
            Assert.Equal(5, Add(2, 2));
        }

        [Theory]
        [InlineData("mongodb://vmsqldev01:27017/", "restaurants", "entities")]
        public void Program(string connectionString, string databaseName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            var collection = database.GetCollection<Entity>(collectionName);

            var entity = new Entity { Name = DateTime.Now.Ticks.ToString() };
            collection.InsertOne(entity);
            var id = entity.Id;

            entity = collection.Find(l => l.Id == id).FirstOrDefault();

            //entity.Name = "Dick";
            //collection.Save(entity);

            //var update = Update.Set("Name", "Harry");
            //collection.Update(query, update);

            var test = collection.DeleteOne(l => l.Id == id);

            var deux = collection.Find(l => l.Id == id).FirstOrDefault();
        }

        private int Add(int x, int y)
        {
            return x + y;
        }
    }

    public class Entity
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }
}
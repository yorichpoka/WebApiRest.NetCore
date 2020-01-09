using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApiRest.NetCore.Repositories.Entities.MongoDB
{
    public class Restaurant : ClassBase
    {
        [BsonElement("address")]
        public Address Address { get; set; }

        [BsonElement("borough")]
        public string Borough { get; set; }

        [BsonElement("cuisine")]
        public string Cuisine { get; set; }

        [BsonElement("restaurant_id")]
        public string Restaurant_Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("grades")]
        public Grade[] Grades { get; set; }

        public BsonDocument ToBsonDocument()
        {
            return this.ToBsonDocument();
        }
    }
}
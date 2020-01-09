using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApiRest.NetCore.Repositories.Entities.MongoDB
{
    public class Address : ClassBase
    {
        [BsonElement("address_id")]
        public string Address_Id { get; set; }

        [BsonElement("building")]
        public string Building { get; set; }

        [BsonElement("coord")]
        public Coordinate Coordinate { get; set; }

        [BsonElement("street")]
        public string Street { get; set; }

        [BsonElement("zipcode")]
        public string ZipCode { get; set; }

        public BsonDocument ToBsonDocument()
        {
            return this.ToBsonDocument();
        }
    }
}
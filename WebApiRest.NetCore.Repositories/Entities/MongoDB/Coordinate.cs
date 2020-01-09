using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApiRest.NetCore.Repositories.Entities.MongoDB
{
    public class Coordinate : ClassBase
    {
        [BsonElement("coordinate_id")]
        public string Coordinate_Id { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("coordinates")]
        [BsonRepresentation(BsonType.Decimal128, AllowTruncation = true)]
        public decimal[] Coordinates { get; set; }

        public BsonDocument ToBsonDocument()
        {
            return this.ToBsonDocument();
        }
    }
}
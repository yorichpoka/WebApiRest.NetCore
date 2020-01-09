using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApiRest.NetCore.Repositories.Entities.MongoDB
{
    public abstract class ClassBase
    {
        [BsonId]
        [BsonElement("Id")]
        public ObjectId Id { get; set; }
    }
}
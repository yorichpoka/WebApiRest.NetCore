using MongoDB.Bson;

namespace WebApiRest.NetCore.Domain.Models.MongoDB
{
    public abstract class ClassBaseModel
    {
        public ObjectId Id { get; set; }
    }
}
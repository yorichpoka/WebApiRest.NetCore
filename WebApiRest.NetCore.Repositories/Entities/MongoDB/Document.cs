using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WebApiRest.NetCore.Repositories.Entities.MongoDB
{
    public class Document : ClassBase
    {
        [BsonElement("document_Id")]
        public string Document_Id { get; set; }

        [BsonElement("fullName")]
        public string FullName { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("extension")]
        public string Extension { get; set; }

        [BsonElement("size")]
        public long Size { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("content_Id")]
        public ObjectId Content_Id { get; set; }

        public BsonDocument ToBsonDocument()
        {
            return this.ToBsonDocument();
        }
    }
}
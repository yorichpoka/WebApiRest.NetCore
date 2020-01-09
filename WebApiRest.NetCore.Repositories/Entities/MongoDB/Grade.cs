using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WebApiRest.NetCore.Repositories.Entities.MongoDB
{
    public class Grade : ClassBase
    {
        [BsonElement("grade_id")]
        public string Grade_Id { get; set; }

        [BsonElement("date")]
        public DateTime? Date { get; set; }

        [BsonElement("grade")]
        public string Name { get; set; }

        [BsonElement("score")]
        public int? Score { get; set; }

        public BsonDocument ToBsonDocument()
        {
            return this.ToBsonDocument();
        }
    }
}
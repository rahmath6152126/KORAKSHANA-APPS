using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GORAKSHANA.Models
{

    public class DataBaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate {get;set;}
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate {get;set;}
    }

}
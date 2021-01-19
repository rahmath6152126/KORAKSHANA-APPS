using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GORAKSHANA.Models
{
    public abstract class BaseModel
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
}
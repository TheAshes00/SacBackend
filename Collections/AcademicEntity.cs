using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SacBackend.Collections
{
    public class AcademicEntity
    {
        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public ObjectId objId { get; set; } 

        [BsonElement("name")]
        public string strName { get; set; } 

        [BsonElement("type")]
        public int intType { get; set; } 

    }
}

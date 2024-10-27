using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SacBackend.Collections
{
    public class Workshop
    {
        [BsonId]
        public ObjectId objId { get; set; }

        [BsonElement("workshop_name")]
        public string strWorkshopName { get; set; }

        [BsonElement("active")]
        public bool boolActive { get; set; } 
        
    }
}

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SacBackend.Collections
{
    public class WorkshopAttendance
    {
        [BsonId]
        public ObjectId objId { get; set; }

        [BsonElement("workshop")]
        public MongoDBRef objWorkshop { get; set; } // Referencia al taller

        [BsonElement("attendance_time")]
        public DateTime dateAttendanceTime { get; set; }
    }

}

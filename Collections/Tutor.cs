using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace SacBackend.Collections
{
    public class Tutor
    {
        [BsonId]
        public ObjectId objId { get; set; }

        [BsonElement("name")]
        public string strName { get; set; }

        [BsonElement("gender")]
        public string strGender { get; set; }

        [BsonElement("workshops")]
        public List<WorkshopSchedule> darrWorkshopsSchedule { get; set; } 

        [BsonElement("active")]
        public bool boolActive { get; set; }

        public class WorkshopSchedule
        {
            [BsonElement("workshop")]
            public MongoDBRef objWorkshop { get; set; }

            [BsonElement("day_of_week")]
            public string strDayOfWeek { get; set; }

            [BsonElement("start_hour")]
            public TimeSpan timeStartHour { get; set; }

            [BsonElement("end_hour")]
            public TimeSpan timeEndHour { get; set; }
        }
    }

}

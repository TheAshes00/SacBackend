using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SacBackend.Collections
{
    public class Admin
    {
        [BsonId]
        public ObjectId objId { get; set; }

        [BsonElement("name")]
        public string strName { get; set; } 

        [BsonElement("user")]
        public string strUser { get; set; } 

        [BsonElement("password")]
        public string strPassword { get; set; }

        [BsonElement("active")]
        public bool boolActive { get; set; } 

        [BsonElement("schedule")]
        public List<Schedule> darrSchedule { get; set; } 

        public class Schedule
        {
            [BsonElement("day_of_week")]
            public string strDayOfWeek { get; set; } 

            [BsonElement("start_hour")]
            public TimeSpan StartHour { get; set; }

            [BsonElement("end_hour")]
            public TimeSpan EndHour { get; set; }
        }
    }
}

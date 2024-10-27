using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SacBackend.Collections
{
    public class Student
    {
        [BsonId]
        public ObjectId objId { get; set; }

        [BsonElement("student_id")]
        public string strStudentId { get; set; }

        [BsonElement("name")]
        public string strName { get; set; }

        [BsonElement("academic_entity")]
        public MongoDBRef AcademicEntity { get; set; }

        [BsonElement("academic_level")]
        public string strAcademicLevel { get; set; }

        [BsonElement("loans")]
        public List<MongoDBRef> darrLoans { get; set; } // Referencia a Loans

        [BsonElement("workshop_attendance")]
        public List<MongoDBRef> darrWorkshopAttendance { get; set; } // Referencia a WorkshopAttendance
    }

}

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using MongoDB.Driver;

namespace SacBackend.Collections
{
    public class Loan
    {
        [BsonId]
        public ObjectId objId { get; set; }

        [BsonElement("loan_date")]
        public DateTime dateLoanDate { get; set; }

        [BsonElement("admin_issued")]
        public MongoDBRef objAdminIssued { get; set; }

        [BsonElement("materials")]
        public List<MongoDBRef> darrMaterials { get; set; }

        [BsonElement("lend_time")]
        public TimeSpan timeLendTime { get; set; }

        [BsonElement("return_time")]
        public TimeSpan? timeReturnTime { get; set; }

        [BsonElement("admin_received")]
        public MongoDBRef? objAdminReceived { get; set; }
    }

}

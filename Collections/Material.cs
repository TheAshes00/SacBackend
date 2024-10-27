using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SacBackend.Collections
{
    public class Material
    {
        [BsonId]
        public ObjectId objId { get; set; }

        [BsonElement("material_id")]
        public string strMaterialId { get; set; }

        [BsonElement("name")]
        public string strName { get; set; }

        [BsonElement("material_type")]
        public string strMaterialType { get; set; }

        [BsonElement("cod_type")]
        public string strCodeType { get; set; }

        [BsonElement("active")]
        public bool boolActive { get; set; }
    }

}

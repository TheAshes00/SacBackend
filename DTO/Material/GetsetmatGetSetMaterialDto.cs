using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SacBackend.DTO.Material
{
    public class GetsetmatGetSetMaterialDto
    {
        public class In
        {
            public string strNumCtrlInt { get; set; }
            public string strName { get; set; }
            public string strMarerialType { get; set; }
            public string strCodeType { get; set; }
            public bool boolActive { get; set; }

        }
    }
}

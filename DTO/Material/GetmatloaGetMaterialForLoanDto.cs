using System.ComponentModel.DataAnnotations;

namespace SacBackend.DTO.Material
{
    public class GetmatloaGetMaterialForLoanDto
    {
        public class In
        {
            [Required]
            public string[] strNumCtrlInt { get; set; }
        }

        public class Out
        {
            public List<ObjMaterial> arrObjMaterial { get; set; }

            public class ObjMaterial
            {
                public int intId { get; set; }
                public string strNumContInt { get; set; }
                public bool boolShowIcon { get; set; }
                public string strName { get; set; }
            }
        }

        
    }
}

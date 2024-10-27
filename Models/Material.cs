using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SacBackend.Models
{
    [Table("Material")]
    public class Material
    {
        [Key]
        [Column("NoCtrlInt", TypeName = "varchar(20)")]
        public string strNumCtrlInt { get; set; }

        [Column("Nombre", TypeName = "varchar(85)")] 
        public string strName { get; set; }

        [Column("TpoMaterial", TypeName = "varchar(60)")] 
        public string strMarerialType { get; set; }

        [Column("TpoCod", TypeName = "varchar(7)")] 
        public string strCodeType { get; set; }

        [Column("Activo", TypeName = "boolean")]
        public bool boolActive { get; set; }

        public ICollection<MaterialLoan> MaterialLoanEntity { get; set; }
    }
}

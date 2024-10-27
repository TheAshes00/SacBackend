using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SacBackend.Models
{
    [Table("material")]
    public class Materiales
    {
        [Key]
        [Column("no_ctrl_int")]
        public string strNumCtrlInt { get; set; }

        [Column("nombre")]
        public string strName { get; set; }

        [Column("tpo_material")]
        public string strMarerialType { get; set; }

        [Column("tpoCod")]
        public string strCodeType { get; set; }
    }
}

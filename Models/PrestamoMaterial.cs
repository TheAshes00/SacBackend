using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SacBackend.Models
{
    [Table("prestamomaterial")]
    public class PrestamoMaterial
    {
        //[Key]
        //[Column("id_registro")]
        //public string strPrestamosPk { get; set; }

        [Column("id_registro")]
        public string strPkLoan { get; set; }

        [Column("no_ctrl_int")]
        public string strPkMaterial { get; set; }
    }
}

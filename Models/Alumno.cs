using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SacBackend.Models
{
    [Table("Alumno")]
    public class Alumno
    {
        [Key]
        [Column("num_cta")]
        public string strNmCta { get; set; }

        [Column("nom_alu")]
        public string strName { get; set; }

        [Column("licenciatura")]
        public string strBachelors { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SacBackend.Models
{
    [Table("Instructor")]
    public class Tutor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Pk")]
        public int intPk { get; set; }

        [Column("NumIdentificacion", TypeName = "varchar(12)")]
        public string strIdentification { get; set; }

        [Column("Nombre", TypeName ="varchar(40)")]
        public string strName { get; set; }

        [Column("Apellido", TypeName = "varchar(60)")]
        public string strSurename { get; set; }

        [Column("Activo", TypeName ="boolean")]
        public bool boolActive { get; set; }

        [Column("Genero", TypeName = "tinyint")]
        public byte byteGender {  get; set; }

        [Column("Carrera", TypeName = "varchar(60)")]
        public string strBachelors { get; set; }

        public virtual ICollection<TutorWorkshop> IcTutorWorkshopEntity { get; set; }
    }
}

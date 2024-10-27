using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SacBackend.Models
{
    [Table("OrganismoAcademico")]
    public class AcademicEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Pk")]
        public int intPk { get; set; }

        [Column("Organismo", TypeName = "varchar(70)")]
        public string strAcademyName { get; set; }

        [Column("Tipo")]
        public int intType { get; set; }
        public virtual ICollection<Student> StudentEntity { get; set; } = new List<Student>();
    }
}

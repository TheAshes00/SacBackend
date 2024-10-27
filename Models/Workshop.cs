using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SacBackend.Models
{
    [Table("Taller")]
    public class Workshop
    {
        [Key]
        [Column("Pk")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int intPk { get; set; }

        [Column("Taller", TypeName ="varchar(60)")]
        public String strWorkshop { get; set; }

        [Column("Activo", TypeName = "boolean")]
        public Boolean boolActive { get; set; } = true;

        public virtual ICollection<TutorWorkshop> IcTutorWorkshopEntity { get; set; }
    }
}

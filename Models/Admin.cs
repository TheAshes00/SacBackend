using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SacBackend.Models
{
    [Table("Administrador")]
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Pk")]
        public int intPk { get; set; }

        [Column("ConAdm", TypeName = "varchar(255)")]
        public string strPass { get; set; }

        [Column("NomAdm", TypeName = "varchar(40)")]
        public string strName { get; set; }

        [Column("Rol", TypeName = "varchar(11)")]
        public string strRole { get; set; }

        [Column("Usuario", TypeName = "varchar(20)")]
        public string strUser { get; set; }

        public virtual ICollection<Loan> IcLoansRegisteredEntity { get; set; }
        public virtual ICollection<Loan> IcLoansReturnedEntity { get; set; }
    }
}

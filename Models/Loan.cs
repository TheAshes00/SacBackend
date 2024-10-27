using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SacBackend.Models
{
    [Table("RegistroPrestamo")]
    public class Loan
    {
        [Key]
        [Column("Pk")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int intPk { get; set; }

        [Column("Dia", TypeName = "date")]
        public DateTime LoanDate { get; set; }

        [Column("HoraInicio", TypeName = "time")]
        public TimeSpan TimeStart { get; set; }

        [Column("HoraFin", TypeName = "time")]
        public TimeSpan TimeEnd { get; set; }

        [Column("AreaEstudio", TypeName = "varchar(35)")]
        public string strStudyArea { get; set; }

        [Column("PkNumCta")]
        [ForeignKey("StudentEntity")]
        public string strPkStudent { get; set; }

        [Column("PkAdministradorEntrega")]
        [ForeignKey("Admin")]
        public int intPkAdminLend { get; set; }

        [Column("PkAdministradorRecibe")]
        [ForeignKey("Admin")]
        public int intPkAdminRecieve { get; set; }

        public Student StuStudent { get; set; }
        public Admin AdAdminRegister { get; set; }
        public Admin AdAdminReturn { get; set; }
        public ICollection<MaterialLoan> IcMaterialLoanEntity { get; set; }
    }

}

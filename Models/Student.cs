using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SacBackend.Models
{
    [Table("Alumno")]
    public class Student
    {
        [Key]
        [Column("NumCta", TypeName = "varchar(12)")]
        public string strNmCta { get; set; }

        [Column("NomAlu", TypeName = "varchar(45)")]
        public string strName { get; set; }

        [Column("ApAlu", TypeName = "varchar(50)")]
        public string strSurename { get; set; }

        [Column("Grado", TypeName = "varchar(40)")]
        public string strBachelors { get; set; }

        [Column("PkOrganismo")]
        [ForeignKey("AcademicEntEntity")]
        public int intPkAcademy { get; set; }

        public AcademicEntity AcademicEntEntity { get; set; } // = new AcademicEntity();

        public virtual ICollection<Loan> IcLoanEntity { get; set; }
        public virtual ICollection<WorkshopAttendance> IcWorkshopAttendanceEntity { get; set; }
    }
}

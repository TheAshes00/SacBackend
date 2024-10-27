using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SacBackend.Models
{
    [Table("AsistenciaTaller")]
    public class WorkshopAttendance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Pk")]
        public int intPk { get; set; }

        [Column("HoraEntrada", TypeName = "time")]
        public TimeSpan TimeCheckInTime { get; set; }

        [Column("Fecha", TypeName = "date")]
        public DateTime DateWorkshopDate { get; set; }

        [Column("PkInstructorTaller")]
        [ForeignKey("TutorWorkshop")]
        public int intPkTutorWorkshop { get; set; }

        [Column("PkAlumno")]
        [ForeignKey("Student")]
        public string strPkStudent { get; set; }

        public Student StudentEntity { get; set; }

        public TutorWorkshop TutorWorkshopEntity { get; set; }
    }
}

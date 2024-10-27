using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SacBackend.Models
{
    [Table("registroprestamo")]
    public class Prestamo
    {
        [Key]
        [Column("id_registro")]
        public string strIdRegistro { get; set; }

        [Column("dia")]
        public DateTime LoanDate { get; set; }

        [Column("hora_inicio")]
        public TimeSpan TimeStart { get; set; }

        [Column("hora_fin")]
        public TimeSpan TimeEnd { get; set; }

        [Column("areaStudio")]
        public string strStudyArea { get; set; }

        [Column("num_cta")]
        public string strPkStudent { get; set; }

        [Column("id_quien_entrego")]
        public int intPkAdminLend { get; set; }

        [Column("id_quien_recibio")]
        public int intPkAdminRecieve { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace SacBackend.DTO.WorkshopAttendance
{
    public class GetsetworattGetSetWorkshopAttendanceDto
    {
        public class In
        {
            [Required]
            public int intPkTutorWorkshop {  get; set; }
            [Required] 
            public string strNmCta { get; set; }
        }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SacBackend.DTO
{
    public class GetsettutGetSetTutorDto
    {
        public class In
        {
            public int? intnPk { get; set; }
            [Required]
            public string strIdentification { get; set; }

            [Required] 
            public string strName { get; set; }

            [Required] 
            public string strSurename { get; set; }

            [Required]
            public bool boolActive { get; set; }

            [Required]
            public byte byteGender { get; set; }


            public string strBachelors { get; set; }
        }

        //--------------------------------------------------------------------------------
    }
}

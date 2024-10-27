using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SacBackend.DTO.Student
{
    public class GetsetstudGetSetStudentDto
    {
        public class In
        {
            [StringLength(12, ErrorMessage = "Numero Cuenta should not exceed 12 characters.")]
            public string strNmCta { get; set; }
            public string strName { get; set; }
            public string strSurename { get; set; }
            public string strBachelors { get; set; }
            public int intPkAcademy { get; set; }
        }




        public class Out
        {
            private readonly String[] arrstrTypes = ["Facultad", "Preparatoria", "Centro Universitario"];
            public String strName { get; set; }
            public string strNmCta { get; set; }
            public String strSurename { get; set; }
            public string strBachelors { get; set; }
            public string strAcademyName { get; set; }
            public string strAcademyType { get; set; }

            public Out(
                String strName_I,
                string strNmCta_I,
                string strSurename_I,
                string strBachelors_I,
                string strAcademyName_I,
                int intType_I
                )
      
    {
                strName = strName_I;
                strNmCta = strNmCta_I;
                strSurename = strSurename_I;
                strBachelors = strBachelors_I;
                strAcademyName = strAcademyName_I;
                strAcademyType = arrstrTypes[intType_I];
            }

        }
    }
}

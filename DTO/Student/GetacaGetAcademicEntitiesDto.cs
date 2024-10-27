using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SacBackend.DTO.Student
{
    public class GetacaGetAcademicEntitiesDto
    {
        public class Out
        {
            public int intPk { get; set; }
            public string strAcademyName { get; set; }
            public int intType { get; set; }
        }
    }
}

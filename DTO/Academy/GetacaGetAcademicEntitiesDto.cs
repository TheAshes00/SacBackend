using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SacBackend.DTO.Academy
{
    public class GetacaGetAcademicEntitiesDto
    {
        public class Out
        {
            public string strGroupName { get; set; }
            public List<Academy> darrAcademies { get; set; }
        }

        public class Academy
        {
            public int intPk { get; set; }
            public string strAcademyName { get; set; }
        }
    }
}

using SacBackend.Context;
using SacBackend.DTO.Student;
using SacBackend.Models;

namespace SacBackend.DAO.Interfaces
{
    public interface StudentDaoInterface
    {
        public List<Student> darrGetAllStudents ( CaafiContext context_I );
        public Student? stuGetStudentByPk ( CaafiContext context_I, string strNmCta_I);
        public void subAddStudent( CaafiContext context_M, Student StStudent_I);
        public void subUpdateStudent( CaafiContext context_M, Student StStudent_I);
        public void subValidateBeforeUpdating(CaafiContext context_I,GetsetstudGetSetStudentDto.In getsetstud_I ,out Student? stStudent_O);    
    }
}

using Microsoft.EntityFrameworkCore;
using SacBackend.Context;
using SacBackend.DAO.Interfaces;
using SacBackend.DTO.Student;
using SacBackend.Models;

namespace SacBackend.DAO
{
    //==================================================================================================================
    public class StudaoStudentDao : StudentDaoInterface
    {
        //--------------------------------------------------------------------------------------------------------------
        //                                                  //ACCESS METHODS.

        //--------------------------------------------------------------------------------------------------------------
        public List<Student> darrGetAllStudents(
            CaafiContext context_I
            )
        {
            return context_I.Student.ToList();
        }

        //--------------------------------------------------------------------------------------------------------------
        public Student? stuGetStudentByPk(
            CaafiContext context_I, 
            string strNmCta_I
            )
        {
            return context_I.Student.FirstOrDefault(st => st.strNmCta.Equals(strNmCta_I));
        }

        //--------------------------------------------------------------------------------------------------------------
        public void subAddStudent(
            CaafiContext context_M, 
            Student StStudent_I
            )
        {
            context_M.Add(StStudent_I);
            context_M.SaveChanges();
        }

        //--------------------------------------------------------------------------------------------------------------
        public void subUpdateStudent(
            CaafiContext context_M, 
            Student StStudent_I
            )
        {
            context_M.Update(StStudent_I);
            context_M.SaveChanges();
        }

        //--------------------------------------------------------------------------------------------------------------
        public void subValidateBeforeUpdating(
            CaafiContext context_I,
            GetsetstudGetSetStudentDto.In getsetstud_I,
            out Student? stStudent_O
            )
        {
            stStudent_O = null;

            if (
                getsetstud_I.strNmCta != null
                )
            {
                stStudent_O = stuGetStudentByPk(context_I, getsetstud_I.strNmCta);
            }
        }
        //--------------------------------------------------------------------------------------------------------------
        public static bool boolValidatePk(
            CaafiContext context_I,
            string strNmCta_I
            )
        {
            return context_I.Student.Where(student => student.strNmCta.Equals(strNmCta_I)).Any();
        }

        //--------------------------------------------------------------------------------------------------------------
        public static Student? stuGetStudentByPkIncludeAcademy(
            CaafiContext context_I,
            string strNmCta_I
            )
        {
            return context_I.Student.
                Where(stu => stu.strNmCta.CompareTo(strNmCta_I) == 0).Include(st => st.AcademicEntEntity).
                FirstOrDefault();
        }

        //--------------------------------------------------------------------------------------------------------------
    }
    //==================================================================================================================
}

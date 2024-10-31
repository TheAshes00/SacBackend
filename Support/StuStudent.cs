using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System.Globalization;
using SacBackend.Context;
using SacBackend.DAO;
using SacBackend.DAO.Interfaces;
using SacBackend.DTO;
using SacBackend.DTO.Material;
using SacBackend.DTO.Student;
using SacBackend.Models;

namespace SacBackend.Support
{
    //==================================================================================================================
    public class StuStudent
    {
        //--------------------------------------------------------------------------------------------------------------
        public static ServansdtoServiceAnswerDto servansSaveStudent(
            CaafiContext context_M,
            GetsetstudGetSetStudentDto.In setstudin_I
            )
        {
            ServansdtoServiceAnswerDto servans;

            StudentDaoInterface studentDao = new StudaoStudentDao();

            Student? stStudent_O;
            studentDao.subValidateBeforeUpdating(context_M, setstudin_I, out stStudent_O);

            if (

                stStudent_O == null
                )
            {
                Student StStudent = new();
                StStudent.strNmCta = setstudin_I.strNmCta;
                StStudent.strName = Tools.Auxiliar.TextHelper.strTitleCase(setstudin_I.strName);
                StStudent.strSurename = Tools.Auxiliar.TextHelper.strTitleCase(setstudin_I.strSurename);
                StStudent.strBachelors = Tools.Auxiliar.TextHelper.strTitleCase(setstudin_I.strBachelors);
                StStudent.intPkAcademy = setstudin_I.intPkAcademy;

                studentDao.subAddStudent(context_M, StStudent);

                servans = new(200, null);
            }
            else
            {
                servans = new(400, "Identification number already asigned to someone, verify your data",
                    "Existent NumCta, cannot add as new student", null);
            }

            return servans;
        }

        //--------------------------------------------------------------------------------------------------------------
        public static ServansdtoServiceAnswerDto servansGetStudent(
            CaafiContext context_I,
            string strNmCta_I
            )
        {
            ServansdtoServiceAnswerDto servans;
            Student? student = context_I.Student.FirstOrDefault(x => x.strNmCta.CompareTo(strNmCta_I) == 0 );
            Student? stStudent = StudaoStudentDao.stuGetStudentByPkIncludeAcademy(context_I, strNmCta_I);

            if (
                stStudent == null
                )
            {
                servans = new(400, "User with identification number " + strNmCta_I + " does not exist. Please try again",
                    "User strNmCta does not exists", "");
            }
            else
            {
                GetsetstudGetSetStudentDto.Out getsetstuout = new GetsetstudGetSetStudentDto.Out(
                    stStudent.strName, 
                    stStudent.strNmCta, 
                    stStudent.strSurename, 
                    stStudent.strBachelors,
                    stStudent.AcademicEntEntity.strAcademyName, 
                    stStudent.AcademicEntEntity.intType
                );

                servans = new(200,""+student?.AcademicEntEntity, "",getsetstuout);
            }

            return servans;
        }

        //--------------------------------------------------------------------------------------------------------------
    }
}

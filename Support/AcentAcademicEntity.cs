using SacBackend.Context;
using SacBackend.DAO.Interfaces;
using SacBackend.DAO;
using SacBackend.DTO;
using SacBackend.Models;
using SacBackend.DTO.Academy;

namespace SacBackend.Support
{
    public class AcentAcademicEntity
    {
        private static string[] arrTypes = ["Facultad", "Preparatoria", "Centro Académico"];
        //--------------------------------------------------------------------------------------------------------------
        public static ServansdtoServiceAnswerDto servansGetAllAcademicEntities(
            CaafiContext context_I
            )
        {
            ServansdtoServiceAnswerDto servans = new ServansdtoServiceAnswerDto(400, "Something Wrong", "", null);

            try
            {
                StudentDaoInterface studentDao = new StudaoStudentDao();
                AcenAcademicEntityInterface acenAcademicEntity = new AcaendaoAcademicEntityDao();

                //List<GetacaGetAcademicEntitiesDto.Out> darrgetacaout =
                //    acenAcademicEntity.darrGetAllAcademicEntities(context_I).Select(
                //        ae => new GetacaGetAcademicEntitiesDto.Out
                //        {
                //            intnPk = ae.intnPk,
                //            intType = ae.intType,
                //            strAcademyName = ae.strAcademyName
                //        }
                //        ).ToList();

                List<GetacaGetAcademicEntitiesDto.Out> darrgetacaout = 
                    acenAcademicEntity.darrGetAllAcademicEntities(context_I).
                    GroupBy(ac => ac.intType).
                    Select(darr => new GetacaGetAcademicEntitiesDto.Out
                    {
                        strGroupName = strGetAcademyType(darr.FirstOrDefault()?.intType),
                        darrAcademies = darr.Select(ac => new GetacaGetAcademicEntitiesDto.Academy
                        {
                            intPk = ac.intPk,
                            strAcademyName = ac.strAcademyName,

                        }).ToList(),
                    }
                    ).ToList();

                servans.strUserMessage = "Ok";
                servans.intStatus = 200;
                servans.objResponse = darrgetacaout;
            }
            catch (Exception ex)
            {
                servans.strDevMessage = ex.Message;
            }

            return servans;
        }

        //--------------------------------------------------------------------------------------------------------------
        private static string strGetAcademyType(
            int? intAcademyType_I
            )
        {
            return arrTypes[intAcademyType_I ?? 0];
        }

        //--------------------------------------------------------------------------------------------------------------
    }
}

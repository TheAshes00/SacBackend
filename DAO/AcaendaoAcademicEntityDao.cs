using System.Security.Cryptography.X509Certificates;
using SacBackend.Context;
using SacBackend.DAO.Interfaces;
using SacBackend.Models;

namespace SacBackend.DAO
{
    //==================================================================================================================
    public class AcaendaoAcademicEntityDao : AcenAcademicEntityInterface
    {
        //--------------------------------------------------------------------------------------------------------------
        //                                                  //ACCESS METHODS.

        //--------------------------------------------------------------------------------------------------------------
        public List<AcademicEntity> darrGetAllAcademicEntities(
            CaafiContext context_I
            )
        {
            return context_I.AcademicEntity.ToList();
        }

        //--------------------------------------------------------------------------------------------------------------
        public AcademicEntity? acenGetAcademicEntityByPk(
            CaafiContext context_I, 
            int intPk_I
            )
        {
            return context_I.AcademicEntity.FirstOrDefault(ac => ac.intPk == intPk_I);
        }

        //--------------------------------------------------------------------------------------------------------------
        public void subAddAcademicEntity(
            CaafiContext context_M, 
            AcademicEntity AcenAcademicEntity_I
            )
        {
            context_M.Add(AcenAcademicEntity_I);
            context_M.SaveChanges();
        }

        //--------------------------------------------------------------------------------------------------------------
        public void subUpdateAcademicEntity(
            CaafiContext context_M, 
            AcademicEntity AcenAcademicEntity_I
            )
        {
            context_M.AcademicEntity.Update(AcenAcademicEntity_I);
            context_M.SaveChanges();
        }

        //--------------------------------------------------------------------------------------------------------------
    }
    //==================================================================================================================
}

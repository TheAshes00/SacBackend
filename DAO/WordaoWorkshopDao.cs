using SacBackend.Context;
using SacBackend.DAO.Interfaces;
using SacBackend.Models;

namespace SacBackend.DAO
{
    //==================================================================================================================
    public class WordaoWorkshopDao : WorintWorkshopInterface
    {
        //--------------------------------------------------------------------------------------------------------------
        //                                                  //ACCESS METHODS.

        //--------------------------------------------------------------------------------------------------------------
        public List<Workshop> darrGetAllWorkshops(
            CaafiContext context_I
            )
        {
            return context_I.Workshop.ToList();
        }

        //--------------------------------------------------------------------------------------------------------------
        public Workshop? stuGetWorkshopByPk(
            CaafiContext context_I, 
            int intPk_I
            )
        {
            return context_I.Workshop.FirstOrDefault(w => w.intPk == intPk_I);
        }

        //--------------------------------------------------------------------------------------------------------------
        public void subAddWorkshop(
            CaafiContext context_M, 
            Workshop Workshop_I
            )
        {
            context_M.Workshop.Add(Workshop_I);
            context_M.SaveChanges();
        }

        //--------------------------------------------------------------------------------------------------------------
        public void subUpdateWorkshop(
            CaafiContext context_M, 
            Workshop Workshop_I
            )
        {
            context_M.Update(Workshop_I);
            context_M.SaveChanges();
        }

        //--------------------------------------------------------------------------------------------------------------
        public static bool boolValidatePk(
            CaafiContext context_I,
            int intPk_I
            )
        {
            bool boolValid = context_I.Workshop.Where( wor => wor.intPk == intPk_I ).Any();
            return boolValid;
        }

        //--------------------------------------------------------------------------------------------------------------

    }
    //==================================================================================================================
}

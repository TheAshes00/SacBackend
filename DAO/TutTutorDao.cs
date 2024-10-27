using SacBackend.Context;
using SacBackend.Models;

namespace SacBackend.DAO
{
    public class TutTutorDao
    {
        //--------------------------------------------------------------------------------
        public static List<Tutor> tutGetAllactiveTutor(
            CaafiContext context_I
            )
        {
            return context_I.Tutor.Where(t => t.boolActive).ToList();
        }

        //--------------------------------------------------------------------------------
        public static List<Tutor> tutGetAllTutor(
            CaafiContext context_I
            )
        {
            return context_I.Tutor.ToList();
        }

        //--------------------------------------------------------------------------------
        public static Tutor? tutGetTutorByPk(
            CaafiContext context_I,
            int intPk_I
            )
        {
            return context_I.Tutor.FirstOrDefault( tut => tut.intPk == intPk_I );
        }

        //--------------------------------------------------------------------------------
        public static void subAdd(
            CaafiContext context_I,
            Tutor tutentity_I
            )
        {
            context_I.Tutor.Add( tutentity_I );
            context_I.SaveChanges();
        }

        //--------------------------------------------------------------------------------
        public static void subUpdate(
            CaafiContext context_I,
            Tutor tutentity_I
            )
        {
            context_I.Tutor.Update(tutentity_I);
            context_I.SaveChanges();
        }

        //--------------------------------------------------------------------------------
        public static bool boolValidatePk(
            CaafiContext context_I,
            int intPk_I
            )
        {
            return context_I.Tutor.Where(tut => tut.intPk != intPk_I).Any();
        }

        //--------------------------------------------------------------------------------
    }
}

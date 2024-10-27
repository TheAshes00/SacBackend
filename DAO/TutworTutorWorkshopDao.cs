using Microsoft.EntityFrameworkCore;
using SacBackend.Context;
using SacBackend.Models;

namespace SacBackend.DAO
{
    public class TutworTutorWorkshopDao
    {
        //--------------------------------------------------------------------------------
        public static List<TutorWorkshop> tutGetAllTutorWorkshop(
            CaafiContext context_I
            )
        {
            return context_I.TutorWorkshop.ToList();
        }

        //--------------------------------------------------------------------------------
        public static TutorWorkshop? tutGetTutorWorkshopByPk(
            CaafiContext context_I,
            int intPk_I
            )
        {
            return context_I.TutorWorkshop.FirstOrDefault(tut => tut.intPk == intPk_I);
        }

        //--------------------------------------------------------------------------------
        public static void subAdd(
            CaafiContext context_I,
            TutorWorkshop tutwokentity_I
            )
        {
            context_I.TutorWorkshop.Add(tutwokentity_I);
            context_I.SaveChanges();
        }

        //--------------------------------------------------------------------------------
        public static void subUpdate(
            CaafiContext context_I,
            TutorWorkshop tutentity_I
            )
        {
            context_I.TutorWorkshop.Update(tutentity_I);
            context_I.SaveChanges();
        }

        //--------------------------------------------------------------------------------
        public static bool boolValidatePk(
            CaafiContext context_I,
            int intPk_I
            )
        {
            return context_I.TutorWorkshop.Where(tut => tut.intPk != intPk_I).Any();
        }

        //--------------------------------------------------------------------------------
        public static void subExcecuteUpdateByPk(
            CaafiContext context_M,
            List<int> darrintPkTutorWorkshop_I,
            bool boolActive_I
            )
        {
            context_M.TutorWorkshop
                .Where(tw => darrintPkTutorWorkshop_I.Contains(tw.intPk))
                .ExecuteUpdate(tw => tw.SetProperty(u => u.boolActive, boolActive_I)
                );
            context_M.SaveChanges();
        }

        //--------------------------------------------------------------------------------
        public static void subExcecuteUpdateByTutor(
            CaafiContext context_M,
            int intPkTutorWorkshop_I,
            bool boolActive_I
            )
        {
            context_M.TutorWorkshop
                .Where(tw => tw.intPkTutor == intPkTutorWorkshop_I)
                .ExecuteUpdate(tw => tw.SetProperty(u => u.boolActive, boolActive_I)
                );
            context_M.SaveChanges();
        }

        //--------------------------------------------------------------------------------
        public static void subExcecuteUpdateByWorkshop(
            CaafiContext context_M,
            int intPkWorkshop_I,
            bool boolActive_I
            )
        {
            context_M.TutorWorkshop
                .Where(tw => tw.intPkWorkshop == intPkWorkshop_I)
                .ExecuteUpdate(tw => tw.SetProperty(u => u.boolActive, boolActive_I)
                );
            context_M.SaveChanges();
        }

        //--------------------------------------------------------------------------------
        public static List<int> subGetTutorWorkshopPk(
            CaafiContext context_I,
            int? intPkTutor_I,
            int? intPkWorkshop_I
            )
        {
            List<int> darrintPkTutorWorkshop = new();
            if (
                intPkTutor_I != null
                )
            {
                //                                          // If tutor is being updated
                //                                          // (Activated), then all workshops
                //                                          // asign to the tutor should be activated
                //                                          // as well, unless, the workshop is 
                //                                          // is also deactivated
                //                                          // In that case, we search for all 
                //                                          // entries where the tutor is assign
                //                                          // then we include the relation with the
                //                                          // workshop entity and performe a new search
                //                                          // where the workshops are also active,
                //                                          // Finally we procced to get all the primary
                //                                          // keys of the tutor woekshop entity 
                //                                          // where the condition is met,and return 
                //                                          // the array of Pks so only those are updates

                darrintPkTutorWorkshop = [.. context_I.TutorWorkshop.Where(
                    tw => tw.intPkTutor == intPkTutor_I)
                    .Include(tw1 => tw1.WorkshopEntity)
                    .Where(tw2 => tw2.WorkshopEntity.boolActive)
                    .Select(tw => tw.intPk)];
            }
            else if (
                intPkWorkshop_I != null
                )
            {
                darrintPkTutorWorkshop = context_I.TutorWorkshop.Where(
                    tw => tw.intPkWorkshop == intPkWorkshop_I)
                    .Include(tw1 => tw1.WorkshopEntity)
                    .Where(tw2 => tw2.WorkshopEntity.boolActive)
                    .Select(tw => tw.intPk)
                    .ToList();
            }

            return darrintPkTutorWorkshop;
        }

        //--------------------------------------------------------------------------------
    }
}

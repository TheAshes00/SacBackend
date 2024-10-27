using SacBackend.Context;
using SacBackend.Models;

namespace SacBackend.DAO
{
    public class WorattWorkshopAttendanceDao
    {
        //--------------------------------------------------------------------------------
        public static void subAdd(
            CaafiContext context_M,
            WorkshopAttendance worattentity_I
            )
        {
            context_M.WorkshopAttendance.Add( worattentity_I );
            context_M.SaveChanges();
        }

        //--------------------------------------------------------------------------------
    }
}

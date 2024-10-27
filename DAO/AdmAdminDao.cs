using SacBackend.Context;
using SacBackend.Models;

namespace SacBackend.DAO
{
    public class AdmAdminDao
    {
        //--------------------------------------------------------------------------------------------------------------
        public static bool boolValidatePk(
            CaafiContext context_I,
            int intPk_I
            )
        {
            return context_I.Admin.Where(admin => admin.intPk == intPk_I).Any();
        }

        //--------------------------------------------------------------------------------
        public static Admin? admGetAdminByPk(
            CaafiContext context_I,
            int intPk_I
            )
        {
            return context_I.Admin.FirstOrDefault(adm => adm.intPk == intPk_I);
        }

        //--------------------------------------------------------------------------------
        public static Admin? admGetAdminByUsername(
            CaafiContext context_I,
            string strUsername_I
            )
        {
            return context_I.Admin.FirstOrDefault(
                adm => adm.strUser.Equals(strUsername_I));
        }

        //--------------------------------------------------------------------------------
        public static void subAddAdmin(
            CaafiContext context_M, 
            Admin admentity
            )
        {
            context_M.Admin.Add(admentity);
            context_M.SaveChanges();
        }

        //--------------------------------------------------------------------------------
        public static List<Admin> admGetAllAdmins(
            CaafiContext context_I
            ) 
        {
            return [.. context_I.Admin.Where(adm => !adm.strRole.Equals("adm"))];
        }

        //--------------------------------------------------------------------------------
    }
}

using SacBackend.Context;
using SacBackend.Models;

namespace SacBackend.DAO
{
    public class MatLoaMaterialLoanDao
    {
        //--------------------------------------------------------------------------------
        public static void subAdd(
            CaafiContext context_M,
            MaterialLoan matloaentity_I
            )
        {
            context_M.MaterialLoan.Add( matloaentity_I );
            context_M.SaveChanges();
        }

        //--------------------------------------------------------------------------------
        
    }
}

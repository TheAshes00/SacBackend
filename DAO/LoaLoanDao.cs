using Microsoft.EntityFrameworkCore;
using SacBackend.Context;
using SacBackend.Models;

namespace SacBackend.DAO
{
    public class LoaLoanDao
    {
        //--------------------------------------------------------------------------------
        public static void subAdd( 
            CaafiContext context_M,
            Loan loanentity
            )
        {
            context_M.Add( loanentity );
            context_M.SaveChanges();
        }

        //--------------------------------------------------------------------------------
        public static void subUpdate(
            CaafiContext context_M,
            Loan loanentity
            )
        {
            context_M.Update(loanentity);
            context_M.SaveChanges();
        }

        //--------------------------------------------------------------------------------
        public static Loan subGetByPk(
            CaafiContext context_I,
            int intPk_I
            )
        {
            return context_I.Loan.Find(intPk_I);
        }

        //--------------------------------------------------------------------------------
        public static List<Loan> loanGetLoanByStudentNmCta(
            CaafiContext context_I,
            string strNmCta_I
            )
        {
            return context_I.Loan.Where(m => m.strPkStudent.Equals(strNmCta_I)).ToList();
        }

        //--------------------------------------------------------------------------------
    }
}

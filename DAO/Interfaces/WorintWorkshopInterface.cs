using SacBackend.Context;
using SacBackend.Models;

namespace SacBackend.DAO.Interfaces
{
    //==================================================================================================================
    public interface WorintWorkshopInterface
    {
        public List<Workshop> darrGetAllWorkshops(CaafiContext context_I);
        public Workshop? stuGetWorkshopByPk(CaafiContext context_I, int intPk_I);
        public void subAddWorkshop(CaafiContext context_M, Workshop Workshop_I);
        public void subUpdateWorkshop(CaafiContext context_M, Workshop Workshop_I);
    }
}

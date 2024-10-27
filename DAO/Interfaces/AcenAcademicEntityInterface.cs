using SacBackend.Context;
using SacBackend.Models;

namespace SacBackend.DAO.Interfaces
{
    public interface AcenAcademicEntityInterface
    {
        public List<AcademicEntity> darrGetAllAcademicEntities(CaafiContext context_I);
        public AcademicEntity? acenGetAcademicEntityByPk(CaafiContext context_I, int intPk);
        public void subAddAcademicEntity(CaafiContext context_M, AcademicEntity AcenAcademicEntity_I);
        public void subUpdateAcademicEntity(CaafiContext context_M, AcademicEntity AcenAcademicEntity_I);
    }
}

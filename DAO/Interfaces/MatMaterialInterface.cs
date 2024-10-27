using SacBackend.Context;
using SacBackend.DTO.Material;
using SacBackend.Models;

namespace SacBackend.DAO.Interfaces
{
    public interface MatMaterialInterface
    {
        public List<Material> darrGetAllMaterials(CaafiContext context_I);
        public Material? matGetMaterialByPk(CaafiContext context_I, string strNumCtrlInt_I);
        public void subAddMaterial(CaafiContext context_M, Material MatMaterial_I);
        public void subUpdateMaterial(CaafiContext context_M, Material MatMaterial_I);
        public void subValidateBeforeUpdating(CaafiContext context_I, GetsetmatGetSetMaterialDto.In getsetmat_I, 
            out Material? MaterialEntity_O);
        public List<Material> darrGetAllMaterialsByPk(CaafiContext context_I, string[] arrstrNumCtrlInt_I);
    }
}

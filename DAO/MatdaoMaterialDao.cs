using SacBackend.Context;
using SacBackend.DAO.Interfaces;
using SacBackend.DTO.Material;
using SacBackend.Models;

namespace SacBackend.DAO
{
    //==================================================================================================================
    public class MatdaoMaterialDao : MatMaterialInterface
    {
        //--------------------------------------------------------------------------------------------------------------
        //                                                  //ACCESS METHODS.

        //--------------------------------------------------------------------------------------------------------------
        public List<Material> darrGetAllMaterials(
            CaafiContext context_I
            )
        {
            return [.. context_I.Material];
        }

        //--------------------------------------------------------------------------------------------------------------
        public Material? matGetMaterialByPk(
            CaafiContext context_I, 
            string strNumCtrlInt_I
            )
        {
            return context_I.Material.FirstOrDefault(m => m.strNumCtrlInt == strNumCtrlInt_I);
        }

        //--------------------------------------------------------------------------------------------------------------
        public void subAddMaterial(
            CaafiContext context_M, 
            Material MatMaterial_I
            )
        {
            context_M.Material.Add(MatMaterial_I);
            context_M.SaveChanges();
        }

        //--------------------------------------------------------------------------------------------------------------
        public void subUpdateMaterial(
            CaafiContext context_M, 
            Material MatMaterial_I
            )
        {
            context_M.Update(MatMaterial_I);
            context_M.SaveChanges();
        }

        //--------------------------------------------------------------------------------------------------------------
        public void subValidateBeforeUpdating(
            CaafiContext context_I, 
            GetsetmatGetSetMaterialDto.In getsetmat_I, 
            out Material? MaterialEntity_O
            )
        {
            MaterialEntity_O = null;
            if (
                getsetmat_I.strNumCtrlInt != null
                )
            {
                MaterialEntity_O = matGetMaterialByPk(context_I, getsetmat_I.strNumCtrlInt);
            }
        }
        //--------------------------------------------------------------------------------------------------------------
        public List<Material> darrGetAllMaterialsByPk(
            CaafiContext context_I, 
            string[] arrstrNumCtrlInt_I
            )
        {
            return [.. context_I.Material.Where(
                m => arrstrNumCtrlInt_I.Contains(
                    m.strNumCtrlInt
                 )
            )];
        }

        //--------------------------------------------------------------------------------------------------------------
        public static bool boolValidateMaterialPk(
            CaafiContext context_I,
            string[] arrstrNumCtrlInt
            )
        {
            //                                              // Get all strNumCtrlInt coincidences
            List<string> darrstrNumCtrlInt = context_I.Material
                                  .Where(m => arrstrNumCtrlInt.Contains(m.strNumCtrlInt))
                                  .Select(m => m.strNumCtrlInt)
                                  .ToList();

            //                                              // Verify if every NumCtrlInt of the array are in the db
            return arrstrNumCtrlInt.All(id => darrstrNumCtrlInt.Contains(id));
        }

        //--------------------------------------------------------------------------------------------------------------
    }
    //==================================================================================================================
}

using SacBackend.Context;
using SacBackend.DAO;
using SacBackend.DTO.Material;
using SacBackend.DTO;
using SacBackend.Models;
using Microsoft.IdentityModel.Tokens;

namespace SacBackend.Support
{
    public class MatMaterial
    {
        //--------------------------------------------------------------------------------------------------------------
        public static ServansdtoServiceAnswerDto servansGetAllMaterialForLoan(
            CaafiContext context_I,
            string[] arrstrNumCtrlInt_I
            )
        {
            ServansdtoServiceAnswerDto servans;
            GetmatloaGetMaterialForLoanDto.Out getmatloa;
            MatdaoMaterialDao materialdao = new MatdaoMaterialDao();

            List<Material> darrMaterial = materialdao.darrGetAllMaterialsByPk(context_I, arrstrNumCtrlInt_I);

            //                                          // Check for at least one element
            bool boolMaterialFound = darrMaterial.Count != 0;

            //                                          // Check for the same length in both arrays
            bool boolAllMaterialCoincidence = darrMaterial.Count == arrstrNumCtrlInt_I.Length;

            //                                          // Check if all material are active
            bool boolAllMaterialActive = darrMaterial.All(objMat => objMat.boolActive);

            if (
                boolMaterialFound &&
                boolAllMaterialCoincidence &&
                boolAllMaterialActive
                )
            {
                //                                          // Using the index from the Select method to create
                //                                          // a new index for the v-for key in Vue frontend
                getmatloa = new GetmatloaGetMaterialForLoanDto.Out
                {
                    arrObjMaterial = darrMaterial.Select(
                        (objMaterial, intIndex) => new GetmatloaGetMaterialForLoanDto.Out.ObjMaterial
                        {
                            boolShowIcon = false,
                            intId = intIndex,
                            strName = objMaterial.strName,
                            strNumContInt = objMaterial.strNumCtrlInt
                        }).ToList()
                };

                servans = new(200, "All found", "Ok", getmatloa);
            }
            else
            {
                subMaterialErrorCheck(arrstrNumCtrlInt_I, darrMaterial, boolMaterialFound, 
                    boolAllMaterialCoincidence, out servans);
            }

            return servans;
        }

        //--------------------------------------------------------------------------------------------------------------
        private static void subMaterialErrorCheck(
            string[] arrstrNumCtrlInt_I,
            List<Material> darrMaterial_I,
            bool boolMaterialFound_I,
            bool boolAllMaterialCoincidence_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            if(
                !boolAllMaterialCoincidence_I || 
                !boolMaterialFound_I
                )
            {
                subMaterialNotFoundErrorMessage(arrstrNumCtrlInt_I, darrMaterial_I, out servans_O);
            }
            else
            {
                subMaterialDeactivatedErrorMessage(arrstrNumCtrlInt_I,darrMaterial_I,out servans_O);
            }
        }

        //--------------------------------------------------------------------------------------------------------------

        private static void subMaterialNotFoundErrorMessage(
            string[] arrstrNumCtrlInt_I,
            List<Material> darrMaterial_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            //                                          // Performance
            HashSet<string> dicnumCtrlIntSet = new(arrstrNumCtrlInt_I);

            //                                          // Get the distinct elements, directly
            //                                          // Map to new array of strings
            //                                          // Get elements that are not in numCtrlIntSet

            string[] arrstrDistinctElements = dicnumCtrlIntSet.Except(
                darrMaterial_I.Select(
                    objMat => objMat.strNumCtrlInt)
                ).ToArray();

            string strUserMessage = String.Format("The following material codes were not found [{0}]. " +
                "Make sure you typed them correctly.",
                string.Join(", ", arrstrDistinctElements)
                );

            servans_O = new(400, strUserMessage, "NmCtrInt not found", arrstrNumCtrlInt_I);
        }

        //--------------------------------------------------------------------------------------------------------------

        private static void subMaterialDeactivatedErrorMessage(
            string[] arrstrNumCtrlInt_I,
            List<Material> darrMaterial_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            //                                          // Get all non active elements, directly
            //                                          // Map to new array of strings
            string[] arrstrNoActiveElements = darrMaterial_I
                .Where(objMaterial => !objMaterial.boolActive)
                .Select(objMaterial => objMaterial.strNumCtrlInt)
                .ToArray();

            string strUserMessage = String.Format("The following material codes have been deactivated [{0}]. " +
                "Inform the manager and wait for further instructions",
                string.Join(", ", arrstrNoActiveElements)
                );

            servans_O = new(400, strUserMessage, "Some NmCtrInt are not active", arrstrNumCtrlInt_I);
        }

        //--------------------------------------------------------------------------------------------------------------
        public static void subPaginateAllMaterial(
            CaafiContext context_I,
            int intPageNumber_I,
            int intPageSize_I,
            string? strSearch_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            MatdaoMaterialDao matdao = new();

            //                                              // Skip represents the number
            //                                              // adn positon of the registers
            //                                              // that will be 
            //                                              // shown, based on th page 
            //                                              // number, and page size
            int intSkip = (intPageNumber_I - 1) * intPageSize_I;

            List<Material> arrmatentity = matdao.darrGetAllMaterials(context_I);

            int intTotalMaterial = arrmatentity.Count;

            List<Material> darrPaginatedMaterials;
            if (
                strSearch_I.IsNullOrEmpty()
                )
            {
                darrPaginatedMaterials = arrmatentity
                    .OrderByDescending(w => w.strNumCtrlInt)
                    .Skip(intSkip)
                    .Take(intPageSize_I)
                    .ToList();
            }
            else
            {
                var darrPaginatedMaterialsFiltered = arrmatentity
                    .Where(cw => cw.strName.Contains(strSearch_I,
                        StringComparison.CurrentCultureIgnoreCase)
                    ).ToList();

                intTotalMaterial = darrPaginatedMaterialsFiltered.Count;

                darrPaginatedMaterials = darrPaginatedMaterialsFiltered
                    .OrderByDescending(w => w.strNumCtrlInt)
                    .Skip(intSkip)
                    .Take(intPageSize_I)
                    .ToList();
            }

            servans_O = new ServansdtoServiceAnswerDto(200,
                Tools.Auxiliar.Paginate.objpagoutPaginateEntity(intTotalMaterial, 
                intPageNumber_I,intPageSize_I, darrPaginatedMaterials));
        }

        //--------------------------------------------------------------------------------------------------------------
        public static void subAddNewMaterial(
            CaafiContext context_I,
            GetsetmatGetSetMaterialDto.In getsetmattin_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            MatdaoMaterialDao matdao = new ();
            if (
                !getsetmattin_I.strMarerialType.IsNullOrEmpty()
                )
            {
                servans_O = new(400, "Invalid data", "intnPk should be null",
                    getsetmattin_I);
            }
            else
            {
                Material matentity = new();

                matentity.strName = Tools.Auxiliar.TextHelper.strTitleCase(
                    getsetmattin_I.strName);

                matentity.strMarerialType = getsetmattin_I.strMarerialType;
                matentity.strCodeType = getsetmattin_I.strCodeType;
                matentity.boolActive = true;


                matdao.subAddMaterial(context_I, matentity);

                servans_O = new(200, null);
            }

        }

        //--------------------------------------------------------------------------------
        public static void subUpdateMaterial(
            CaafiContext context_I,
            GetsetmatGetSetMaterialDto.In getsetmattin_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            MatdaoMaterialDao matdao = new ();
            Material? matentity = matdao.matGetMaterialByPk(context_I, 
                getsetmattin_I.strNumCtrlInt);
            
            if (
                getsetmattin_I.strNumCtrlInt.IsNullOrEmpty() ||
                matentity == null
                )
            {
                servans_O = new(400, "Invalid data", "intnPk should not be null",
                    getsetmattin_I);
            }
            else
            {

                matentity.strName = Tools.Auxiliar.TextHelper.strTitleCase(
                    getsetmattin_I.strName);

                matentity.strMarerialType = getsetmattin_I.strMarerialType;
                matentity.strCodeType = getsetmattin_I.strCodeType;
                matentity.boolActive = getsetmattin_I.boolActive;

                matdao.subUpdateMaterial(context_I, matentity);

                servans_O = new(200, null);
            }

        }

        //--------------------------------------------------------------------------------------------------------------
    }
}

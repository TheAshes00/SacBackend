using Microsoft.AspNetCore.Mvc;
using SacBackend.Context;
using SacBackend.DTO.Material;
using SacBackend.DTO;
using SacBackend.Support;
using VueAppTest1.Server.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace SacBackend.Controllers
{
    //==================================================================================================================
    [ApiController]
    [Route("[controller]")]
    public class MaterialController : ControllerBase
    {
        //--------------------------------------------------------------------------------------------------------------
        //                                          INSTANCE VARIABLES
        //--------------------------------------------------------------------------------------------------------------
        private readonly ILogger<MaterialController> _logger;

        //-------------------------------------------------------------------------------------------------------------
        //                                                  //CONSTRUCTORS.
        public MaterialController (ILogger<MaterialController> logger)
        {
            _logger = logger;
        }

        //--------------------------------------------------------------------------------------------------------------
        [HttpGet("[action]")]
        public IActionResult GetAllMaterial(
            [FromQuery]
            string[] arrstrNumCtrlInt
            )
        {
            using var context = new CaafiContext();

            ServansdtoServiceAnswerDto servansdto = 
                MatMaterial.servansGetAllMaterialForLoan(context, arrstrNumCtrlInt);
            IActionResult aresult = base.Ok(servansdto);
            return aresult;
        }

        //--------------------------------------------------------------------------------
        [Authorize]
        [HttpGet("[action]")]
        public IActionResult GetPaginatedMaterial(
            [FromQuery]
            int intPageNumber,
            int intPageSize,
            string? strSearch
            )
        {
            CaafiContext context = new CaafiContext();

            ServansdtoServiceAnswerDto servans;

            try
            {

                MatMaterial.subPaginateAllMaterial(context, intPageNumber, intPageSize,
                    strSearch, out servans);
            }
            catch (Exception ex)
            {
                servans = new(400, "Something wrong", ex.Message, ex.Data);
            }

            IActionResult aresult = base.Ok(servans);
            return aresult;
        }


        //--------------------------------------------------------------------------------
        [Authorize]
        [HttpPost("[action]")]
        public IActionResult UpdateMaterial(
            [FromBody]
            GetsetmatGetSetMaterialDto.In getsetmattin
            )
        {
            ServansdtoServiceAnswerDto servans;
            if (
                !ModelState.IsValid
                )
            {
                servans = new(400, "Invalid data", "Invalid ModelState",
                    ModelState.Keys.ToString());
            }
            else
            {
                CaafiContext context = new CaafiContext();
                using var transaction = context.Database.BeginTransaction();

                transaction.CreateSavepoint("beforeMaterialUpdate");

                try
                {
                    MatMaterial.subUpdateMaterial(context, getsetmattin, out servans);
                    if (
                        servans.intStatus == 200
                        )
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.RollbackToSavepoint("beforeMaterialUpdate");
                    }
                }
                catch (Exception ex)
                {
                    transaction.RollbackToSavepoint("beforeMaterialUpdate");
                    servans = new(400, "Something wrong", ex.Message, ex.Data);
                }
            }
            IActionResult aresult = base.Ok(servans);
            return aresult;
        }

        //--------------------------------------------------------------------------------
        [Authorize]
        [HttpPost("[action]")]
        public IActionResult SetMaterial(
            [FromBody]
            GetsetmatGetSetMaterialDto.In getsetmattin
            )
        {
            ServansdtoServiceAnswerDto servans;
            if (
                !ModelState.IsValid
                )
            {
                servans = new(400, "Invalid data", "Invalid ModelState",
                    ModelState.Keys.ToString());
            }
            else
            {
                CaafiContext context = new CaafiContext();
                using var transaction = context.Database.BeginTransaction();

                transaction.CreateSavepoint("beforeMaterialAdd");

                try
                {
                    MatMaterial.subAddNewMaterial(context, getsetmattin, out servans);
                    if (
                        servans.intStatus == 200
                        )
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.RollbackToSavepoint("beforeMaterialAdd");
                    }
                }
                catch (Exception ex)
                {
                    transaction.RollbackToSavepoint("beforeMaterialAdd");
                    servans = new(400, "Something wrong", ex.Message, ex.Data);
                }
            }

            IActionResult aresult = base.Ok(servans);
            return aresult;
        }

        //--------------------------------------------------------------------------------
    }
}

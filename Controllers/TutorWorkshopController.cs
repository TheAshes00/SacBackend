using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SacBackend.Context;
using SacBackend.DTO;
using SacBackend.DTO.TutorWorkshop;
using SacBackend.Support;

namespace SacBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TutorWorkshopController : ControllerBase
    {
        //--------------------------------------------------------------------------------
        //                                 //INSTANCE VARIABLES
        //--------------------------------------------------------------------------------
        private readonly ILogger<TutorWorkshopController> _logger;

        //--------------------------------------------------------------------------------
        //                                 //CONSTRUCTORS.
        public TutorWorkshopController(ILogger<TutorWorkshopController> logger)
        {
            _logger = logger;
        }

        //--------------------------------------------------------------------------------
        [Authorize]
        [HttpPost("[action]")]
        public IActionResult SetTutorWorkshop(
            [FromBody]
            GetsettutwokGetSetTutorWorkshopDto getsettutwor
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

                transaction.CreateSavepoint("beforeTutorWorkshopAdd");


                try
                {
                    TutworTutorWorkshop.subAddNewTutorWorkshop(context, getsettutwor, 
                        out servans);
                    if (
                        servans.intStatus == 200
                        )
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.RollbackToSavepoint("beforeTutorWorkshopAdd");
                    }
                }
                catch (Exception ex)
                {
                    transaction.RollbackToSavepoint("beforeTutorWorkshopAdd");
                    servans = new(400, "Something wrong", ex.Message, ex.Data);
                }
            }

            IActionResult aresult = base.Ok(servans);
            return aresult;
        }

        //--------------------------------------------------------------------------------
        [Authorize]
        [HttpPost("[action]")]
        public IActionResult UpdateTutorWorkshop(
            [FromBody]
            GetsettutwokGetSetTutorWorkshopDto getsettutwor
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

                transaction.CreateSavepoint("beforeTutorWorkshopUpdate");


                try
                {
                    TutworTutorWorkshop.subUpdateTutorWorkshop(context, getsettutwor, 
                        out servans);

                    if (
                        servans.intStatus == 200
                        )
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.RollbackToSavepoint("beforeTutorWorkshopUpdate");
                    }
                }
                catch (Exception ex)
                {
                    transaction.RollbackToSavepoint("beforeTutorWorkshopUpdate");
                    servans = new(400, "Something wrong", ex.Message, ex.Data);
                }
            }
            IActionResult aresult = base.Ok(servans);
            return aresult;
        }

        //--------------------------------------------------------------------------------
        [Authorize]
        [HttpGet("[action]")]
        public IActionResult GetPaginatedTutorWorkshops(
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
                TutworTutorWorkshop.GetPaginatedTutorWorkshop(context, intPageNumber, 
                    intPageSize, strSearch, out servans);
            }
            catch (Exception ex)
            {
                servans = new(400, "Something wrong", ex.Message, ex.Data);
            }

            IActionResult aresult = base.Ok(servans);
            return aresult;
        }

        //--------------------------------------------------------------------------------
        [HttpGet("[action]/{intPkWorkshop}")]
        public IActionResult GetWorkshopTutor(
            int intPkWorkshop
            )
        {
            CaafiContext context = new CaafiContext();

            ServansdtoServiceAnswerDto servans;

            try
            {
                TutworTutorWorkshop.subGetWorkshopTutor(context, intPkWorkshop, 
                    out servans);
            }
            catch (Exception ex)
            {
                servans = new(400, "Something wrong", ex.Message, ex.Data);
            }

            IActionResult aresult = base.Ok(servans);
            return aresult;
        }

        //--------------------------------------------------------------------------------
    }
}

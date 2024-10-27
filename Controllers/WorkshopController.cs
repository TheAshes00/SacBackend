using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using SacBackend.Context;
using SacBackend.DTO;
using SacBackend.DTO.Workshop;
using SacBackend.DTO.WorkshopAttendance;
using SacBackend.Models;
using SacBackend.Support;

namespace SacBackend.Controllers
{
    //==================================================================================================================
    [Route("/[controller]")]
    [ApiController]
    public class WorkshopController : ControllerBase
    {
        //--------------------------------------------------------------------------------
        //                                INSTANCE VARIABLES
        //--------------------------------------------------------------------------------
        private readonly ILogger<WorkshopController> _logger;
        private IConfiguration _configuration;

        //--------------------------------------------------------------------------------
        //                                //CONSTRUCTORS.
        public WorkshopController(ILogger<WorkshopController> logger, 
            IConfiguration iConfig)
        {
            _logger = logger;
            _configuration = iConfig;
        }

        //--------------------------------------------------------------------------------
        [Authorize]
        [HttpPost("[action]")]
        public IActionResult SetWorkshop(
            GetSetWorkshopDto.In getworkshop
            )
        {
            CaafiContext context = new CaafiContext();
            using var transaction = context.Database.BeginTransaction();

            ServansdtoServiceAnswerDto servans;

            try
            {
                WorWorkshop.subAddNewWorkshop(context, getworkshop, out servans);
                if (
                    servans.intStatus == 200
                    )
                {
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                servans = new(400, "Something wrong", ex.Message, ex.Data);
            }

            IActionResult aresult = base.Ok(servans);
            return aresult;
        }


        //--------------------------------------------------------------------------------
        [Authorize]
        [HttpGet("[action]")]
        public IActionResult GetPaginatedWorkshops(
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

                WorWorkshop.subGetPaginatedWorkshops(context,intPageNumber,intPageSize,
                    strSearch,out servans);
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
        public IActionResult UpdateWorkshop(
            [FromBody]
            GetSetWorkshopDto.In getworkshop
            )
        {
            CaafiContext context = new CaafiContext();
            using var transaction = context.Database.BeginTransaction();

            ServansdtoServiceAnswerDto servans;

            try
            {
                WorWorkshop.subUpdate(context, getworkshop, out servans);
                if (
                    servans.intStatus == 200
                    )
                {
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                servans = new(400, "Something wrong", ex.Message, ex.Data);
            }

            IActionResult aresult = base.Ok(servans);
            return aresult;
        }


        //--------------------------------------------------------------------------------
        [HttpGet("[action]")]
        public IActionResult GetAllActiveWorkshops(
            )
        {
            CaafiContext context = new CaafiContext();

            ServansdtoServiceAnswerDto servans;

            try
            {

                WorWorkshop.subGetAllActiveWorkshops(context, out servans);
            }
            catch (Exception ex)
            {
                servans = new(400, "Something wrong", ex.Message, ex.Data);
            }

            IActionResult aresult = base.Ok(servans);
            return aresult;
        }

        //--------------------------------------------------------------------------------
        [HttpPost("[action]")]
        public IActionResult SetWorkshopAttendance(
            [FromBody]
            GetsetworattGetSetWorkshopAttendanceDto.In getsetworatt
            )
        {
            DateTime dateNow = DateTime.Now;

            CaafiContext context = new CaafiContext();
            using var transaction = context.Database.BeginTransaction();
            transaction.CreateSavepoint("beforeAddWorkshopAttendance");
            ServansdtoServiceAnswerDto servans;

            try
            {
                if (
                !ModelState.IsValid
                )
                {
                    servans = new ServansdtoServiceAnswerDto(400, "Invalid data",
                        "Invalid Modelstate", ModelState.Keys);
                }
                else
                {
                    WorWorkshop.subSetWorkshopAttendance(context, getsetworatt, dateNow,
                        out servans);

                    if (
                        servans.intStatus == 200
                        )
                    {
                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex) 
            {
                transaction.RollbackToSavepoint("beforeAddWorkshopAttendance");
                servans = new(400, "Error", ex.Message,ex.StackTrace);
            }

            IActionResult iresult = base.Ok(servans);
            return iresult;
        }

        //--------------------------------------------------------------------------------
    }
}

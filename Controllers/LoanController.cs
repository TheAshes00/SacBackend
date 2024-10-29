using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using SacBackend.Context;
using SacBackend.DTO;
using SacBackend.DTO.Loan;
using SacBackend.Support;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SacBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        //--------------------------------------------------------------------------------
        //                                   INSTANCE VARIABLES
        //--------------------------------------------------------------------------------
        private readonly ILogger<LoanController> _logger;

        //--------------------------------------------------------------------------------
        //                                   //CONSTRUCTORS.
        public LoanController(ILogger<LoanController> logger)
        {
            _logger = logger;
        }

        //--------------------------------------------------------------------------------
        [HttpPost("[action]")]
        public IActionResult RegisterLoan(
            [FromBody] 
            SetloaregSetLoanRegisterDto.In setloaregin
            )
        {
            ServansdtoServiceAnswerDto servans;
            CaafiContext context = new CaafiContext();

            using var transaction = context.Database.BeginTransaction();
            try
            {
                if (
                    !ModelState.IsValid
                )
                {
                    servans = new ServansdtoServiceAnswerDto(400, 
                        "Something wrong, missing info", "Invalida model for POST request", 
                        ModelState.Values);
                }
                else
                {
                    LoaLoan.subRegisterLoan(context, setloaregin, out servans);

                    if(
                        servans.intStatus == 200
                        )
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
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
        [HttpGet("[action]/{strNmCta}")]
        public IActionResult MaterialToReturn(
            [FromRoute]
            string strNmCta
            )
        {
            ServansdtoServiceAnswerDto servans;

            CaafiContext context = new CaafiContext();

            try
            {
                LoaLoan.subReturnMaterial(context, strNmCta, out servans);
            }
            catch (Exception ex) 
            {
                servans = new(400, "Something wrong", ex.Message, ex.TargetSite);
            }
            
            IActionResult aresult = base.Ok(servans);
            return aresult;
        }

        //--------------------------------------------------------------------------------
        [HttpPost("[action]")]
        public IActionResult ReturnMaterial(
            [FromBody]
            SetloaregSetLoanRegisterDto.In setloareg
            )
        {
            ServansdtoServiceAnswerDto servans;

            CaafiContext context = new CaafiContext();

            using var transaction = context.Database.BeginTransaction();
            try
            {
                LoaLoan.subReturnmaterial(context, setloareg, out servans);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                servans = new(400, "Something wrong", ex.Message, ex.TargetSite);
            }

            IActionResult aresult = base.Ok(servans);
            return aresult;
        }

        //--------------------------------------------------------------------------------

    }
}

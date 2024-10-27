using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;
using SacBackend.Collections;
using SacBackend.Context;
using SacBackend.DTO;
using SacBackend.DTO.Material;
using SacBackend.DTO.Student;
using SacBackend.Models;
using SacBackend.MongoConfig;
using SacBackend.Support;

namespace VueAppTest1.Server.Controllers
{
    //==================================================================================================================
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        //--------------------------------------------------------------------------------------------------------------
        //                                          INSTANCE VARIABLES
        //--------------------------------------------------------------------------------------------------------------
        private readonly ILogger<StudentController> _logger;
        private IConfiguration _configuration;
        private readonly MongoDbService _mongoDbService;

        //-------------------------------------------------------------------------------------------------------------
        //                                                  //CONSTRUCTORS.
        public StudentController(ILogger<StudentController> logger,IConfiguration iConfig, MongoDbService mongoDbService)
        {
            _logger = logger;
            _configuration = iConfig;
            _mongoDbService = mongoDbService;
        }
        
        //--------------------------------------------------------------------------------------------------------------
        [HttpGet("[action]/{strNmCta}")]
        public IActionResult GetStudent(
            string strNmCta
            )
        {
            using var context = new CaafiContext();

            ServansdtoServiceAnswerDto servansdto = StuStudent.servansGetStudent(context, strNmCta);
            IActionResult aresult = base.Ok(servansdto);
            return aresult;
        }

        //--------------------------------------------------------------------------------------------------------------
        [HttpPost("[action]")]
        public IActionResult SetStudent(
            [FromBody]
            GetsetstudGetSetStudentDto.In getstudtoin
            )
        {
            ServansdtoServiceAnswerDto servansdto;
            using var context = new CaafiContext();
            using var transaction = context.Database.BeginTransaction();

            try
            {
                servansdto = StuStudent.servansSaveStudent(context,getstudtoin);
                transaction.Commit();
            }
            catch (
                Exception ex
            )
            { 
                servansdto = new ServansdtoServiceAnswerDto(400,ex.Message);
                transaction.Rollback();
            }
            
            IActionResult aresult = base.Ok(servansdto);
            return aresult;
        }

        //--------------------------------------------------------------------------------------------------------------
        [HttpGet("[action]")]
        public IActionResult GetAllAcademicEntities(
            )
        {
            using var context = new CaafiContext();

            ServansdtoServiceAnswerDto servansdto = AcentAcademicEntity.servansGetAllAcademicEntities(context);
            IActionResult aresult = base.Ok(servansdto);
            return aresult;
        }

        //--------------------------------------------------------------------------------------------------------------
        [HttpGet("[action]/{strNmCta}")]
        public IActionResult GetOneStudent(
            string strNmCta
            )
        {
            ServansdtoServiceAnswerDto servansdto;

            try
            {
                // Obtener la colección de estudiantes desde el servicio MongoDbService
                var studentCollection = _mongoDbService.GetStudentCollection();

                // Filtrar por el campo "student_id" (que no es el ObjectId)
                var filter = Builders<SacBackend.Collections.Student>.Filter.Eq("student_id", strNmCta);

                // Buscar el estudiante que coincida con el student_id
                var student = studentCollection.Find(filter).FirstOrDefault();

                servansdto = new(200, student);
            }
            catch (
                Exception ex
            )
            {
                servansdto = new ServansdtoServiceAnswerDto(400, ex.Message);
                
            }

            IActionResult aresult = base.Ok(servansdto);
            return aresult;
        }

        //--------------------------------------------------------------------------------------------------------------
        [HttpGet("[action]")]
        public IActionResult GetAcademies(
            )
        {
            ServansdtoServiceAnswerDto servansdto;

            try
            {
                // Obtener la colección de estudiantes desde el servicio MongoDbService
                var studentCollection = _mongoDbService.GetAcademicEntitiesCollection();

                var tutors = studentCollection.Find(_ => true).ToList();

                servansdto = new(200, tutors);
            }
            catch (
                Exception ex
            )
            {
                servansdto = new ServansdtoServiceAnswerDto(400, ex.Message);

            }

            IActionResult aresult = base.Ok(servansdto);
            return aresult;
        }

        //--------------------------------------------------------------------------------------------------------------
        [HttpGet("[action]")]
        public IActionResult GetAdmins(
            )
        {
            ServansdtoServiceAnswerDto servansdto;

            try
            {
                // Obtener la colección de estudiantes desde el servicio MongoDbService
                string[] strAdminsNames = ["Lorena Dorado", "Diana Arriaga", "Vanessa Avalos"];

                MAdAdmin.servansAddAdmins(strAdminsNames, _mongoDbService ,out servansdto);
            }
            catch (
                Exception ex
            )
            {
                servansdto = new ServansdtoServiceAnswerDto(400, ex.Message);

            }

            IActionResult aresult = base.Ok(servansdto);
            return aresult;
        }

        //--------------------------------------------------------------------------------------------------------------
        [HttpGet("[action]")]
        public IActionResult ImportStudents(
            )
        {
            ServansdtoServiceAnswerDto servansdto;
            Caafi2Context context2 = new Caafi2Context();
            try
            {
                MStuStudents.servansImport(context2, _mongoDbService, out servansdto);
            }
            catch (
                Exception ex
            )
            {
                servansdto = new ServansdtoServiceAnswerDto(400, ex.Message);

            }

            IActionResult aresult = base.Ok(servansdto);
            return aresult;
        }

        //--------------------------------------------------------------------------------------------------------------
        [HttpGet("[action]")]
        public IActionResult ImportLoans(
            )
        {
            ServansdtoServiceAnswerDto servansdto;
            Caafi2Context context2 = new Caafi2Context();
            try
            {
                MStuStudents.servansImportLoans(context2, _mongoDbService, out servansdto);
            }
            catch (
                Exception ex
            )
            {
                servansdto = new ServansdtoServiceAnswerDto(400, ex.Message);

            }

            IActionResult aresult = base.Ok(servansdto);
            return aresult;
        }

        //--------------------------------------------------------------------------------------------------------------
        [HttpGet("[action]")]
        public IActionResult ImportMaterials(
            )
        {
            ServansdtoServiceAnswerDto servansdto;
            Caafi2Context context2 = new Caafi2Context();
            try
            {
                MStuStudents.servansImportMaterial(_mongoDbService, context2, out servansdto);
            }
            catch (
                Exception ex
            )
            {
                servansdto = new ServansdtoServiceAnswerDto(400, ex.Message);

            }

            IActionResult aresult = base.Ok(servansdto);
            return aresult;
        }

        //--------------------------------------------------------------------------------------------------------------
        [HttpGet("[action]")]
        public IActionResult ExportLoans(
            )
        {
            ServansdtoServiceAnswerDto servansdto;
            
            try
            {
                MStuStudents.servansExportLoans(_mongoDbService, out servansdto);
            }
            catch (
                Exception ex
            )
            {
                servansdto = new ServansdtoServiceAnswerDto(400, ex.Message);

            }

            IActionResult aresult = base.Ok(servansdto);
            return aresult;
        }

        //--------------------------------------------------------------------------------------------------------------


    }
    //==================================================================================================================
}

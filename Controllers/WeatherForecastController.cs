using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SacBackend;
using SacBackend.Context;
using SacBackend.DTO;
using SacBackend.Models;

namespace SacBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("[action]")]
        public ServansdtoServiceAnswerDto GetTest()
        {
            ServansdtoServiceAnswerDto servansdto;
            try
            {
                List<AcademicEntity> darrWorkshop = new List<AcademicEntity>();
                CaafiContext caafi = new CaafiContext();

                var v = caafi.Student.Include("AcademicEntEntity").ToList();

                List<Student> student = caafi.Student.ToList();

                darrWorkshop = caafi.AcademicEntity.ToList();

                var y = darrWorkshop.Find(s => s.intPk == 1);

                servansdto = new ServansdtoServiceAnswerDto(200,
                    student.ToList().Select(n => new
                    {
                        name = n.strName,
                        academy = n.AcademicEntEntity.strAcademyName,
                    }));
            }
            catch (Exception ex)
            {
                servansdto = new ServansdtoServiceAnswerDto(400, ex.Message);
            }



            return servansdto;
        }
    }
}

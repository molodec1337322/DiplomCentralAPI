using ArduinoUploader.Hardware;
using ArduinoUploader;
using DiplomCentralAPI.Controllers.Utils;
using Microsoft.AspNetCore.Mvc;
using DiplomCentralAPI.Data.Repository.Postgres;
using DiplomCentralAPI.Data.Interfaces;
using DiplomCentralAPI.Data.Models;

namespace DiplomCentralAPI.Controllers
{
    [Route("api/experiments")]
    public class ExperimentsController : Controller
    {
        private readonly IRepository<Schema> _schemaRepository;

        public ExperimentsController(IRepository<Schema> schemaRepo) 
        {
            _schemaRepository = schemaRepo;
        }

        [HttpGet]
        [Route("getAllList")]
        public IActionResult GetExperimentsList()
        {
            /*
            //тут вообще должно все браться из БД, но пока затычка будет

            List<ExperimentData> data = new List<ExperimentData>
            {
                new ExperimentData("Name of experement 1", "Used: 2 times"),
                new ExperimentData("Name of experement 2", "Used: 3 times"),
                new ExperimentData("Name of experement 3", "Used: 5 times"),
                new ExperimentData("Name of experement 4", "Used: 546 times")
            };
            */

            List<ExperimentData> data = 


            return Ok(data);
        }

        [HttpGet]
        [Route("createNewSchema")]
        public IActionResult CreateNewSchemaExperiment()
        {

            return Ok();
        }

        public record ExperimentData(string Name, string Data);
    }
}

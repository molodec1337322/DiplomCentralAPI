using ArduinoUploader.Hardware;
using ArduinoUploader;
using DiplomCentralAPI.Controllers.Utils;
using Microsoft.AspNetCore.Mvc;

namespace DiplomCentralAPI.Controllers
{
    [Route("api/experiments")]
    public class ExperimentsController : Controller
    {
        public ExperimentsController() { }

        [HttpGet]
        [Route("getExperimentsList")]
        public IActionResult GetExperimentsList()
        {
            //тут вообще должно все браться из БД, но пока затычка будет

            List<ExperimentData> data = new List<ExperimentData>
            {
                new ExperimentData("Name of experement 1", "Used: 2 times"),
                new ExperimentData("Name of experement 2", "Used: 3 times"),
                new ExperimentData("Name of experement 3", "Used: 5 times"),
                new ExperimentData("Name of experement 4", "Used: 546 times")
            };

            return Ok(data);
        }

        public record ExperimentData(string Name, string Data);
    }
}

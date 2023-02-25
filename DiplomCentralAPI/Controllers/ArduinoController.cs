using Microsoft.AspNetCore.Mvc;

namespace DiplomCentralAPI.Controllers
{
    [Route("api/arduino")]
    public class ArduinoController : Controller
    {
        public ArduinoController() { }

        [HttpPost]
        [Route("/setCommand")]
        public IActionResult SetCommand() 
        {

            return Ok();
        }

    }
}

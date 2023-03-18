using Microsoft.AspNetCore.Mvc;
using System.IO.Ports;

namespace DiplomCentralAPI.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route("/Home")]
        [Route("/Home/Index")]
        [Route("/api/getCOMPorts")]
        public IActionResult Index() 
        {
            return Ok(new { content = SerialPort.GetPortNames() });
        }
    }
}

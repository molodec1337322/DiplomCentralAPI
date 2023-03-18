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
            List<Port> ports = new List<Port>();
            List<string> portsList = SerialPort.GetPortNames().ToList();
            foreach(string port in portsList)
            {
                ports.Add(new Port(port));
            }

            return Ok(ports);
        }

        record Port(string port);
    }
}

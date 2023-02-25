using ArduinoUploader;
using ArduinoUploader.Hardware;
using DiplomCentralAPI.Controllers.Utils;
using Microsoft.AspNetCore.Mvc;

namespace DiplomCentralAPI.Controllers
{
    [Route("api/arduino")]
    public class ArduinoController : Controller
    {
        public ArduinoController() { }

        [HttpPost]
        [Route("/setCommand")]
        public IActionResult SetCommand(int USBPort) 
        {
            string port = "COM" + USBPort;

            ArduinoMegaProgramm programm = new ArduinoMegaProgramm();
            string programmString = programm.GetProgramm(100, 100, 100);

            var uploader = new ArduinoSketchUploader(
                new ArduinoSketchUploaderOptions()
                {
                    PortName = port,
                    ArduinoModel = ArduinoModel.Mega2560
                });

            uploader.UploadSketch();

            return Ok();
        }

    }
}

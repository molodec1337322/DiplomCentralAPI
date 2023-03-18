using ArduinoUploader;
using ArduinoUploader.Hardware;
using DiplomCentralAPI.Controllers.Utils;
using Microsoft.AspNetCore.Mvc;
using System.IO.Ports;
using System.Windows;

namespace DiplomCentralAPI.Controllers
{
    [Route("api/arduino")]
    public class ArduinoController : Controller
    {
        public ArduinoController() { }

        [HttpPost]
        [Route("uploadProgramm")]
        public IActionResult UploadProgramm(int USBPort) 
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

        [HttpGet]
        [Route("setCommands")]
        public IActionResult SetCommands(int USBPort, int Direction, int Deformation, int PauseDuration)
        {
            Console.WriteLine("USB port: " + USBPort + " Direction: " + Direction + " Deformation: " + Deformation + " Pause duration: " + PauseDuration);
            try
            {
                SerialPort serialPort = new SerialPort();

                serialPort.PortName = "COM" + USBPort;
                serialPort.BaudRate = 9600;
                serialPort.Open();

                //("Direction Deformation Duration")
                serialPort.Write(Direction + " " + Deformation + " " + PauseDuration);

                serialPort.Close();
            }
            catch(Exception ex)
            {
                return BadRequest(new {error = ex.Message });
            }
            

            return Ok(new { content = "USB port: " + USBPort + " Direction: " + Direction + " Deformation: " + Deformation + " Pause duration: " + PauseDuration });
        }

    }
}

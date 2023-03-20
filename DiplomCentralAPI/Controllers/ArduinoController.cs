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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="USBPort">Номер ком порта</param>
        /// <param name="Direction">Направление деформации</param>
        /// <param name="Deformation">Дальность деформации</param>
        /// <param name="PauseDuration">задержка между шагами деформации</param>
        /// <param name="Side">сторона деформации (0 - 7, или -1 если не нужна)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("setCommands")]
        public IActionResult SetCommands(string USBPort, int Direction, int Deformation, int PauseDuration, short Side)
        {
            Console.WriteLine("USB port: " + USBPort + " Direction: " + Direction + " Deformation: " + Deformation + " Pause duration: " + PauseDuration + " Side: " + Side);
            try
            {
                SerialPort serialPort = new SerialPort();

                serialPort.PortName = USBPort;
                serialPort.BaudRate = 9600;
                serialPort.Open();

                //("Direction Deformation Duration")
                serialPort.Write(Direction + " " + Deformation + " " + PauseDuration + " " + Side * 2);

                serialPort.Close();
            }
            catch(Exception ex)
            {
                return BadRequest(new {error = ex.Message });
            }
            

            return Ok(new { content = "USB port: " + USBPort + " Direction: " + Direction + " Deformation: " + Deformation + " Pause duration: " + PauseDuration + " Side: " + Side });
        }

    }
}

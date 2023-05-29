using ArduinoUploader;
using ArduinoUploader.Hardware;
using DiplomCentralAPI.Controllers.Utils;
using DiplomCentralAPI.Data.Interfaces;
using DiplomCentralAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO.Ports;


namespace DiplomCentralAPI.Controllers
{
    [Route("api/arduino")]
    public class ArduinoController : Controller
    {
        private readonly IRepository<Schema> _schemaRepository;
        private readonly IConfiguration _appConfiguration;
        private readonly IWebHostEnvironment _appEnviroment;

        public ArduinoController(IRepository<Schema> schemaRepo, IConfiguration appConfiguration, IWebHostEnvironment appEnviroment) 
        {
            _schemaRepository = schemaRepo;
            _appConfiguration = appConfiguration;
            _appEnviroment = appEnviroment;
        }

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
        [Route("setArduino")]
        public IActionResult SetArduinoInit(string USBPort, int side1, int side2, int side3, int side4, int side5, int side6, int side7, int side8, short isSaving)
        {
            try
            {
                SerialPort serialPort = new SerialPort();

                serialPort.PortName = USBPort;
                serialPort.BaudRate = 9600;
                serialPort.Open();

                //("Direction Deformation Duration")
                serialPort.Write(0 + " " + 0 + " " + 0 + " " + 0 + " " + 0 + " " + 0 + " " + 0 + " " + 0 + " " + isSaving * 0);

                serialPort.Close();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }

            string schemaText = side1 + " " + side2 + " " + side3 + " " + side4 + " " + side5 + " " + side6 + " " + side7 + " " + side8 + " " + isSaving;
            Schema experimentSchema = _schemaRepository.GetAll().FirstOrDefault(s => s.Text == schemaText);

            Double duration = 7.0;

            int durationInt = (int)Math.Ceiling(duration);

            return RedirectToAction("StartVideoRecord", "Camera", new { cameraId = 0, width = 640, height = 480, framerate = 30, duration = durationInt, experimentId = experimentSchema.Id, USBPort = USBPort });
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
        //[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
        public IActionResult SetCommands(string USBPort, int side1, int side2, int side3, int side4, int side5, int side6, int side7, int side8, short isSaving)
        {
            //Console.WriteLine("USB port: " + USBPort + " Direction: " + Direction + " Deformation: " + Deformation + " Pause duration: " + PauseDuration + " Side: " + Side);
            try
            {
                SerialPort serialPort = new SerialPort();

                serialPort.PortName = USBPort;
                serialPort.BaudRate = 9600;
                serialPort.Open();

                //("Direction Deformation Duration")
                serialPort.Write(side1 + " " + side2 + " " + side3 + " " + side4 + " " + side5 + " " + side6 + " " + side7 + " " + side8 + " " + isSaving);

                serialPort.Close();
            }
            catch(Exception ex)
            {
                return BadRequest(new {error = ex.Message });
            }

            string schemaText = side1 + " " + side2 + " " + side3 + " " + side4 + " " + side5 + " " + side6 + " " + side7 + " " + side8 + " " + isSaving;
            Schema experimentSchema = _schemaRepository.GetAll().FirstOrDefault(s => s.Text == schemaText);

            Double duration = 3.0;

            int durationInt = (int)Math.Ceiling(duration);

            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="USBPort">Номер ком порта</param>
        /// <param name="ExperimentId">Id эксперимента из БД</param>
        /// <returns></returns>
        [HttpGet]
        [Route("setCommandsByExperimentId")]
        public IActionResult SetCommandByExperementId(string USBPort, int ExperimentId)
        {
            //Console.WriteLine("USB port: " + USBPort + " Direction: " + Direction + " Deformation: " + Deformation + " Pause duration: " + PauseDuration + " Side: " + Side);
            try
            {
                SerialPort serialPort = new SerialPort();

                serialPort.PortName = USBPort;
                serialPort.BaudRate = 9600;
                serialPort.Open();

                //("Direction Deformation Duration")
                Schema experimentSchema = _schemaRepository.Get(ExperimentId);
                string[] schemaSplittedText = experimentSchema.Text.Split(" ");
                serialPort.Write(schemaSplittedText[0] + " " + schemaSplittedText[1] + " " + schemaSplittedText[2] + " " + schemaSplittedText[3] + " " +
                    schemaSplittedText[4] + " " + schemaSplittedText[5] + " " + schemaSplittedText[6] + " " + schemaSplittedText[7] + " " +
                    Int32.Parse(schemaSplittedText[8]));

                serialPort.Close();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }

            return Ok();
        }

    }
}

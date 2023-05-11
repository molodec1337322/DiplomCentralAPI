using DiplomCentralAPI.Data.Interfaces;
using DiplomCentralAPI.Data.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Npgsql.Internal.TypeHandlers;
using System.Diagnostics;
using System.IO.Ports;

namespace DiplomCentralAPI.Controllers
{
    [Route("api/camera")]
    public class CameraController : Controller
    {
        private readonly IRepository<Schema> _schemaRepository;
        private readonly IRepository<Experiment> _experimentRepository;
        private readonly IConfiguration _appConfiguration;
        private readonly IWebHostEnvironment _appEnviroment;

        public CameraController(IRepository<Schema> schemaRepo, IRepository<Experiment> experimentRepo, IConfiguration appConfiguration, IWebHostEnvironment appEnviroment)
        {
            _schemaRepository = schemaRepo;
            _experimentRepository = experimentRepo;
            _appConfiguration = appConfiguration;
            _appEnviroment = appEnviroment;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cameraId"> id камеры</param>
        /// <param name="width"> ширина видео в пикселях</param>
        /// <param name="height"> высота видео</param>
        /// <param name="framerate"> фреймрейт</param>
        /// <param name="duration"> длительность видео в секундах</param>
        /// <returns></returns>
        [HttpGet]
        [Route("startVideoRecord")]
        public async Task<IActionResult> StartVideoRecord(int cameraId, int width, int height, int framerate, int duration, int experimentId, int USBPort)
        {
            var cameraScriptPath = Path.Combine(_appEnviroment.ContentRootPath, "Cameras", "Camera1", "videoRecord.py");
            if (!System.IO.File.Exists(cameraScriptPath))
            {
                return BadRequest(new {error = "camera script not found"});
            }
            var videoRecordSavePath = Path.Combine(_appEnviroment.WebRootPath, _appConfiguration.GetValue<string>("VideoFilesStorageFolder"));
            if (!Directory.Exists(videoRecordSavePath))
            {
                //return BadRequest(new { error = "camera script not found" });
                Directory.CreateDirectory(videoRecordSavePath);
            }
            var videoRecordVideoFile = Path.Combine(videoRecordSavePath, Guid.NewGuid().ToString() + ".avi");

            Experiment newExperiment = new Experiment();
            newExperiment.StartedAt = DateTime.UtcNow;
            newExperiment.VideoPath = videoRecordVideoFile;
            newExperiment.ResultPath = "";
            newExperiment.SchemaId = experimentId;
            newExperiment.MyHandlerId = null;

            var cameraStartedCallbackUrl = "https://" + Request.Host.Host + ":" + Request.Host.Port + "/api/arduino/setCommandsByExperimentId?" + "USBPort=" + USBPort + "&experimentId=" + experimentId;

            try
            {
                char[] splitter = { '\r' };

                Process process = new Process();
                process.StartInfo.FileName = "py.exe";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;

                process.StartInfo.Arguments = string.Concat(cameraScriptPath, " ", videoRecordVideoFile, " ", 0, " ", width.ToString(), " ", height.ToString(), " ", framerate.ToString(), " ", duration.ToString(), " ", cameraId.ToString(), " ", cameraStartedCallbackUrl);

                process.OutputDataReceived += (sender, e) => {
                        Console.WriteLine("Received output: " + e.Data);
                };

                process.Start();
                process.BeginOutputReadLine();

                process.WaitForExit();
            }
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
                return BadRequest(new { error = ex.Message, traceback = ex.StackTrace });
            }

            newExperiment.EndedAt = DateTime.UtcNow;
            _experimentRepository.Add(newExperiment);
            _experimentRepository.SaveChanges();

            return Ok("videoRecordOk");
        }
    }
}

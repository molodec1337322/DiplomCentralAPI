using DiplomCentralAPI.Data.Interfaces;
using DiplomCentralAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql.Internal.TypeHandlers;
using System.Diagnostics;

namespace DiplomCentralAPI.Controllers
{
    [Route("api/camera")]
    public class CameraController : Controller
    {
        private readonly IRepository<Schema> _schemaRepository;
        private readonly IConfiguration _appConfiguration;
        private readonly IWebHostEnvironment _appEnviroment;

        public CameraController(IRepository<Schema> schemaRepo, IConfiguration appConfiguration, IWebHostEnvironment appEnviroment)
        {
            _schemaRepository = schemaRepo;
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
        public IActionResult StartVideoRecord(int cameraId, int width, int height, int framerate, int duration)
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

            string urlToStopRecord = "https://" + Request.Host.Host + ":" + Request.Host.Port + "/api/camera/stopVideoRecord";
            try
            {
                /*
                var engine = Python.CreateEngine();
                var scope = engine.CreateScope();

                ICollection<string> searchPaths = engine.GetSearchPaths();
                searchPaths.Add("C:\\Python34\\Lib");
                searchPaths.Add("C:\\Python34\\Lib\\site-packages");
                engine.SetSearchPaths(searchPaths);

                engine.ExecuteFile(cameraScriptPath, scope);
                var recordVideo = scope.GetVariable("record_video");
                var result = recordVideo(videoRecordSavePath, false, width, height, framerate, duration, urlToStopRecord);
                */

                char[] splitter = { '\r' };

                Process process = new Process();
                process.StartInfo.FileName = "python.exe";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;

                // call hello.py to concatenate passed parameters
                process.StartInfo.Arguments = string.Concat(cameraScriptPath, " ", videoRecordVideoFile, " ", 0, " ", width.ToString(), " ", height.ToString(), " ", framerate.ToString(), " ", duration.ToString(), " ", urlToStopRecord, " ", cameraId.ToString());
                process.Start();

                StreamReader sReader = process.StandardOutput;
                string[] output = sReader.ReadToEnd().Split(splitter);

                foreach (string s in output)
                    Console.WriteLine(s);

                process.WaitForExit();
            }
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
                return BadRequest(new { error = ex.Message });
            }
            
            return Ok();
        }

        [HttpGet]
        [Route("stopVideoRecord")]
        public IActionResult StopVideoRecord(int cameraId)
        {
            return Ok();
        }
    }
}

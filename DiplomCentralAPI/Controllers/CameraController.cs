using DiplomCentralAPI.Data.Interfaces;
using DiplomCentralAPI.Data.Models;
using IronPython.Hosting;
using Microsoft.AspNetCore.Mvc;

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
            var cameraScriptPath = Path.Combine(_appEnviroment.ContentRootPath, "Cameras", "Camera1", "main.py");
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
            string urlToStopRecord = Request.Host.Host + Request.Host.Port + "api/camera/stopVideoRecord";
            try
            {
                var engine = Python.CreateEngine();
                //var source = engine.CreateScriptSourceFromFile(Path.Combine(cameraScrriptPath));
                var scope = engine.CreateScope();

                engine.ExecuteFile(cameraScriptPath, scope);
                var recordVideo = scope.GetVariable("record_video");
                var result = recordVideo(videoRecordSavePath, false, width, height, framerate, duration, urlToStopRecord);
            }
            catch(Exception ex)
            {
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

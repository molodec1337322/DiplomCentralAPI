using DiplomCentralAPI.Data.Interfaces;
using DiplomCentralAPI.Data.Models;
using IronPython.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace DiplomCentralAPI.Controllers
{
    [Route("api/Camera")]
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
        [HttpPost]
        [Route("startVideoRecord")]
        public IActionResult StartVideoRecord(int cameraId, int width, int height, int framerate, int duration)
        {
            var cameraScrriptPath = Path.Combine(Path.Combine(_appEnviroment.WebRootPath, "Cameras", "Camera1", "main.py"));
            if (!Directory.Exists(cameraScrriptPath))
            {
                return BadRequest(new {error = "camera script not found"});
            }
            var videoRecordSavePath = _appConfiguration.GetValue<string>("VideoFilesStorageFolder");
            if (!Directory.Exists(videoRecordSavePath))
            {
                return BadRequest(new { error = "camera script not found" });
            }

            try
            {
                var engine = Python.CreateEngine();
                //var source = engine.CreateScriptSourceFromFile(Path.Combine(cameraScrriptPath));
                var scope = engine.CreateScope();

                engine.ExecuteFile(cameraScrriptPath, scope);
                var recordVideo = scope.GetVariable("record_video");
                var result = recordVideo(videoRecordSavePath, false, width, height, framerate, duration);
            }
            catch(Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            
            return Ok();
        }
    }
}

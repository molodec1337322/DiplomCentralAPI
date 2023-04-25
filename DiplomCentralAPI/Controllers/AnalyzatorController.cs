using DiplomCentralAPI.Data.Interfaces;
using DiplomCentralAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiplomCentralAPI.Controllers
{
    [Route("api/analyzator")]
    public class AnalyzatorController : Controller
    {
        private readonly IRepository<Schema> _schemaRepository;
        private readonly IRepository<Experiment> _experimentRepository;
        private readonly IConfiguration _appConfiguration;
        private readonly IWebHostEnvironment _appEnviroment;

        public AnalyzatorController(IRepository<Schema> schemaRepo, IRepository<Experiment> experimentRepo, IConfiguration appConfiguration, IWebHostEnvironment appEnviroment)
        {
            _schemaRepository = schemaRepo;
            _experimentRepository = experimentRepo;
            _appConfiguration = appConfiguration;
            _appEnviroment = appEnviroment;
        }

        [HttpGet]
        [Route("getAllAnalyzators")]
        public async Task<IActionResult> GetAllAnalyzators(string analyzerName, int experimentId)
        {
            return Ok();
        }

        [HttpGet]
        [Route("analyze")]
        public async Task<IActionResult> StartAnalyze(string analyzerName, int experimentId)
        {
            return Ok();
        }
    }
}

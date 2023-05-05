using ArduinoUploader.Hardware;
using ArduinoUploader;
using DiplomCentralAPI.Controllers.Utils;
using Microsoft.AspNetCore.Mvc;
using DiplomCentralAPI.Data.Repository.Postgres;
using DiplomCentralAPI.Data.Interfaces;
using DiplomCentralAPI.Data.Models;

namespace DiplomCentralAPI.Controllers
{
    [Route("api/experiments")]
    public class ExperimentsController : Controller
    {
        private readonly IRepository<Schema> _schemaRepository;
        private readonly IRepository<Experiment> _experimentsRepository;

        public ExperimentsController(IRepository<Schema> schemaRepo, IRepository<Experiment> experimentRepo) 
        {
            _schemaRepository = schemaRepo;
            _experimentsRepository = experimentRepo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getAllSchemasList")]
        public IActionResult GetExperimentsList()
        {
            List<Schema> data = _schemaRepository.GetAll().ToList();


            return Ok(data);
        }

        /// <summary>
        /// Req {"description": "new experement schema", "videoSaveFolderPath": "C:/wwww/root", "schemaText": "100 102 150 -1"}
        /// Res {}
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("createNewSchema")]
        public async Task<IActionResult> CreateNewSchemaExperimentAsync()
        {
            try
            {
                NewSchemaData? newSchemaData = await HttpContext.Request.ReadFromJsonAsync<NewSchemaData>();

                Schema newSchema = new Schema();
                newSchema.Description = newSchemaData.description;
                newSchema.Text = newSchemaData.schemaText;
                newSchema.CreatedAt = DateTime.UtcNow;

                _schemaRepository.Add(newSchema);
                await _schemaRepository.SaveChanges();

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getAllExperimentsList")]
        public async Task<IActionResult> GetAllExperementsList()
        {
            List<Experiment> data = _experimentsRepository.GetAll().ToList();
            return Ok(data);
        }

        public record ExperimentData(string Name, string Data);
        public record NewSchemaData(string description, string videoSaveFolderPath, string schemaText);
    }
}

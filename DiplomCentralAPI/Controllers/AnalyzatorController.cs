using DiplomCentralAPI.Data.Interfaces;
using DiplomCentralAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiplomCentralAPI.Controllers
{
    [Route("api/analyzator")]
    public class AnalyzatorController : Controller
    {
        private readonly IRepository<Schema> _schemaRepository;

        public AnalyzatorController(IRepository<Schema> schemaRepo)
        {
            _schemaRepository = schemaRepo;
        }
    }
}

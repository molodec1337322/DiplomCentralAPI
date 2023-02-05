using DiplomCentralAPI.Data.Interfaces;
using DiplomCentralAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiplomCentralAPI.Controllers
{
    [Route("api/Handler")]
    public class HandlerController : Controller
    {
        private readonly IRepository<Handler> _handlerRepository;

        public HandlerController(IRepository<Handler> handlerRepository) 
        { 
            _handlerRepository = handlerRepository;
        }

        [HttpGet]
        [Route("Test")]
        public async Task<IActionResult> Test()
        {
            return Ok("Darova, cheliki");
        }
    }
}

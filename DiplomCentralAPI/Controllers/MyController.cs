using Microsoft.AspNetCore.Mvc;

namespace DiplomCentralAPI.Controllers
{
    //[Route("api/[controller]")]
    public class MyController : Controller
    {

        public MyController() { }


        public IActionResult MyTest() 
        {
            return new ObjectResult("Test");
        }
    }
}

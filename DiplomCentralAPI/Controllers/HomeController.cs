using Microsoft.AspNetCore.Mvc;

namespace DiplomCentralAPI.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route("/Home")]
        [Route("/Home/Index")]
        public string Index() 
        {
            return "test";
        }
    }
}

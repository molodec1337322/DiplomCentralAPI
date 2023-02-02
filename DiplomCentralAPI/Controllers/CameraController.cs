using Microsoft.AspNetCore.Mvc;

namespace DiplomCentralAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CameraController : Controller
    {
        public CameraController() { }

        public string Start()
        {
            return "camera started";
        }

        public string Stop()
        {
            return "camera stopped";
        }
    }
}

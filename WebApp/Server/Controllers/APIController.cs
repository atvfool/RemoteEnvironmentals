using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers
{
    public class APIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ping(PingModel parameters)
        {
            return new JsonResult(parameters);
        }
        [HttpGet]
        public JsonResult list()
        {
            return new JsonResult("");
        }
        [HttpGet]
        public JsonResult GetSettings()
        {
            return new JsonResult("");
        }
        [HttpPost]
        public JsonResult SaveSettings(SettingsModel settings)
        {
            return new JsonResult(settings);
        }

    }
}

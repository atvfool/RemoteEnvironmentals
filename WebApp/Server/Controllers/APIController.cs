using Microsoft.AspNetCore.Mvc;
using Server.Classes;
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
            Database db = new Database();
            return new JsonResult(db.SavePing(parameters));
        }
        [HttpGet]
        public JsonResult list()
        {
            Database db = new Database();
            return new JsonResult(db.GetPings());
        }
        [HttpGet]
        public JsonResult GetSettings(SettingsModel parameters)
        {
            Database db = new Database();
            return new JsonResult(db.GetSettings());
        }
        [HttpPost]
        public JsonResult SaveSettings(SettingsModel settings)
        {
            Database db = new Database();
            return new JsonResult(db.SaveSettings(settings));
        }

    }
}

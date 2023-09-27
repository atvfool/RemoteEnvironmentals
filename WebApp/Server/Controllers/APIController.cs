using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Utilities;
using Utilities.Models;

namespace Server.Controllers
{
    public class APIController : Controller
    {
        private string GetPreSharedKey()
        {
            var presharedkey = Environment.GetEnvironmentVariable("PRESHAREDKEY");
            if (presharedkey == null)
            {
                Console.WriteLine("You must set your 'PRESHAREDKEY' environment variable.");
                Environment.Exit(0);
            }
            return presharedkey;
        }
        [HttpPost]
        public JsonResult ping(PingModel parameters, string key)
        {
            bool result = false;
            if(GetPreSharedKey() == key)
            {
                Database db = new Database();
                result = db.SavePing(parameters);
                
            }
            return new JsonResult("{success: " + result.ToString() + "}");

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
        public JsonResult SaveSettings(SettingsModel settings, string key)
        {
            bool result = false;
            if (GetPreSharedKey() == key)
            {
                Database db = new Database();
                result = db.SaveSettings(settings);
                return new JsonResult("\"{success: " + result.ToString() + "}\"");
            }
            else
            {
                return new JsonResult("\"{success: " + result.ToString() + ", 'reason': 'key mismatch'}\"");
            }

            
        }

        [HttpGet]
        public JsonResult GetLatestPing(string location)
        {
            Database db = new Database();
            return new JsonResult(db.GetLatestPing(location));
        }

    }
}

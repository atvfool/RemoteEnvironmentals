﻿using Microsoft.AspNetCore.Mvc;
using Utilities;
using Utilities.Models;
using Server.Models;
using System.Diagnostics;

namespace Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Database db = new Database();
            List<PingModel> pings = db.GetPings();
            return View(pings);
        }

        public IActionResult Settings()
        {
            Database db = new Database();
            SettingsModel model= db.GetSettings();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
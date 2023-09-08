using MedicalStaff.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace MedicalStaff.Controllers
{
    public class StartUpController : Controller
    {
        private readonly ILogger<StartUpController> logger;

        public StartUpController(ILogger<StartUpController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult HomePage()
        {
            return this.View(new LoginViewModel());
        }

        [HttpPost]
        public IActionResult Login()
        {
            if (!this.ModelState.IsValid)
                return this.RedirectToAction("HomePage");

            return this.View();
        }

        public IActionResult MedicalRecordsPage()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
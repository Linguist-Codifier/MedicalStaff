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
        public IActionResult SignIn()
        {
            return this.View(new SignInViewModel());
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return this.View(new SignUpViewModel());
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
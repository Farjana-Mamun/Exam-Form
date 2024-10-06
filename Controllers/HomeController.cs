using ExamForms.Manager;
using ExamForms.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExamForms.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly TemplateManager templateManager;

        public HomeController(ILogger<HomeController> logger
            , TemplateManager templateManager)
        {
            this.logger = logger;
            this.templateManager = templateManager;
        }

        public async Task<IActionResult> Index()
        {
            var templates = await templateManager.GettAllTemplateAsync();
            return View(templates);
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

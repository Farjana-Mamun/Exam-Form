using Microsoft.AspNetCore.Mvc;

namespace ExamForms.Areas.Templates.Controllers
{
    [Area(nameof(Templates))]
    public class TemplateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace ExamForms.Areas.Templates.Controllers
{
    [Area(nameof(Templates))]
    public class QuestionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddQuestionModal()
        {
            return PartialView("~/Areas/Templates/Views/Shared/_TemplateQuestionAddModal.cshtml");
        }
    }
}

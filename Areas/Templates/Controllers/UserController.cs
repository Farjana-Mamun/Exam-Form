using ExamForms.Manager;
using ExamForms.Models.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExamForms.Areas.Templates.Controllers
{
    public class UserController : Controller
    {
        private readonly TemplateManager templateManager;
        private readonly QuestionManager questionManager;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(TemplateManager templateManager
            , QuestionManager questionManager
            , UserManager<ApplicationUser> userManager)
        {
            this.templateManager = templateManager;
            this.questionManager = questionManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

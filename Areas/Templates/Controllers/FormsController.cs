using ExamForms.Manager;
using ExamForms.Models.Accounts;
using ExamForms.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExamForms.Areas.Templates.Controllers
{
    [Area(nameof(Templates))]
    public class FormsController : Controller
    {
        private readonly TemplateManager templateManager;
        private readonly QuestionManager questionManager;
        private readonly FormsManager formManager;
        private readonly UserManager<ApplicationUser> userManager;

        public FormsController(TemplateManager templateManager
            , QuestionManager questionManager
            , FormsManager formManager
            , UserManager<ApplicationUser> userManager)
        {
            this.templateManager = templateManager;
            this.questionManager = questionManager;
            this.formManager = formManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(int id)
        {
            FormViewModel formViewModel = new FormViewModel();
            formViewModel.Questions = await questionManager.GetQuestionsByTemplateIdAsync(id);
            formViewModel.TemplateId = formViewModel.Questions.FirstOrDefault().TemplateId;
            return View(formViewModel);
        }

        public async Task<IActionResult> SubmitForm(FormViewModel model)
        {
            await formManager.SaveFormAsync(model, User.Identity);
            return View();
        }
    }
}

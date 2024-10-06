using ExamForms.Constants;
using ExamForms.Manager;
using ExamForms.Models;
using ExamForms.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace ExamForms.Areas.Templates.Controllers
{
    [Area(nameof(Templates))]
    public class QuestionController : Controller
    {
        private readonly QuestionManager questionManager;

        public QuestionController(QuestionManager questionManager)
        {
            this.questionManager = questionManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddQuestionModal()
        {
            QuestionViewModel model = new QuestionViewModel();
            return PartialView("~/Areas/Templates/Views/Shared/_TemplateQuestionAddModal.cshtml", model);
        }

        public IActionResult AddQuestionCheckbox(int questionType)
        {
            QuestionViewModel questionViewModel = new QuestionViewModel();
            if (questionType == ((int)Enums.TemplateQuestionTypeEnum.Checkbox))
            {
                List<QuestionOptionViewModel> detail = new List<QuestionOptionViewModel>();
                detail.Add(new QuestionOptionViewModel()
                {
                    QuestionOptionId = 1,
                    OptionName = "Yes",
                    IsCorrectAnswer = true,
                });
                detail.Add(new QuestionOptionViewModel()
                {
                    QuestionOptionId = 2,
                    OptionName = "No",
                    IsCorrectAnswer = false,
                });
                questionViewModel.QuestionOptions = detail;
            }
            return PartialView("~/Areas/Templates/Views/Shared/_AddQuestionCheckboxModal.cshtml", questionViewModel);
        }

        public async Task<IActionResult> AddTemplateQuestion(QuestionViewModel model)
        {
            model.QuestionId = await questionManager.AddTemplateQuestionAsync(model, User.Identity);
            List<QuestionViewModel> questions = new List<QuestionViewModel>();
            questions = await questionManager.GetAllQuestionsAsync();

            return PartialView("~/Areas/Templates/Views/Shared/_TemplateQuestionList.cshtml", questions);
        }
    }
}

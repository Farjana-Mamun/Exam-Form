using ExamForms.Constants;
using ExamForms.Models;
using ExamForms.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

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
        public IActionResult AddQuestionCheckbox(int questionType)
        {
            QuestionViewModel questionViewModel = new QuestionViewModel();
            if (questionType == ((int)Enums.TemplateQuestionTypeEnum.Checkbox))
            {
                List<QuestionOptionViewModel> detail = new List<QuestionOptionViewModel>();
                detail.Add(new QuestionOptionViewModel()
                {
                    OptionName = "Yes",
                    IsCorrectAnswer = true,
                });
                detail.Add(new QuestionOptionViewModel()
                {
                    OptionName = "No",
                    IsCorrectAnswer = false,
                });
                questionViewModel.QuestionOptions = detail;
            }
            return PartialView("~/Areas/Templates/Views/Shared/_AddQuestionCheckboxModal.cshtml", questionViewModel);
        }
    }
}

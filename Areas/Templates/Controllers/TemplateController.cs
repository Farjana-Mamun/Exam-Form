using ExamForms.Manager;
using ExamForms.Models;
using ExamForms.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamForms.Areas.Templates.Controllers
{
    [Area(nameof(Templates))]
    public class TemplateController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        public readonly TemplateManager templateManager;
        private readonly QuestionManager questionManager;

        public TemplateController(TemplateManager _templateManager
            , QuestionManager questionManager
            , IWebHostEnvironment _webHostEnvironment)
        {
            templateManager = _templateManager;
            this.questionManager = questionManager;
            webHostEnvironment = _webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            TemplateViewModel template = new TemplateViewModel();

            var topics = await templateManager.GettAllTopic();
            ViewBag.Topics = new SelectList(topics, "TopicId", "TopicName");
            ViewBag.Tags = await templateManager.GettAllTag();

            template.Questions = await questionManager.GetAllQuestionsAsync();
            return View(template);
        }

        [HttpPost]
        public async Task<IActionResult> SaveTemplateSetup(TemplateViewModel model, IFormFile? Image)
        {
            if (Image != null)
            {
                string folder = "images/templateImages/";
                folder += Guid.NewGuid().ToString() + Image.FileName;
                string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folder);
                Image.CopyTo(new FileStream(serverFolder, FileMode.Create));
                model.Image = "/" + folder;
            }
            model.TemplateId = await templateManager.CreateTemplateAsync(model, User.Identity);
            return Json(new { message = "Success", id = model.TemplateId });
        }
    }
}

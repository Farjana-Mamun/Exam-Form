using ExamForms.Manager;
using ExamForms.Models;
using ExamForms.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
        public async Task<IActionResult> Index()
        {
            var templates = await templateManager.GettAllTemplateAsync();
            return View(templates);
        }

        public async Task<IActionResult> SaveTemplate(int id, string tab = "Setup")
        {
            TemplateViewModel template = new TemplateViewModel();
            template = await templateManager.GetTemplateByIdAsync(id) ?? new TemplateViewModel();

            var topics = await templateManager.GettAllTopic();
            ViewBag.Topics = new SelectList(topics, "TopicId", "TopicName");
            ViewBag.Tags = await templateManager.GettAllTag();
            ViewBag.ActiveTab = tab;

            template.Questions = await questionManager.GetQuestionsByTemplateIdAsync(id);
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

            if (model.TemplateId == 0)
                model.TemplateId = await templateManager.CreateTemplateAsync(model, User.Identity);
            else
                model.TemplateId = await templateManager.UpdateTemplateAsync(model, User.Identity);
            
            return RedirectToAction("SaveTemplate", new { id = model.TemplateId, tab = "Questions" });
            //return Json(new { message = "Success", id = model.TemplateId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTemplate(int id)
        {
            await templateManager.DeleteTemplateAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            var template = await templateManager.GetTemplateByIdAsync(id);
            var comments = await templateManager.GetCommentsByTemplateIdAsync(id);
            var likes = await templateManager.GetLikesByTemplateIdAsync(id);

            var templateDetailsViewModel = new TemplateDetailsViewModel
            {
                Template = template,
                Comments = comments,
                LikesCount = likes.Count
            };

            return View(templateDetailsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentViewModel comment)
        {
            var result = await templateManager.AddCommentAsync(comment);
            return RedirectToAction("Details", new { id = comment.TemplateId });
        }

        [HttpPost]
        public async Task<IActionResult> AddLike(LikeViewModel like)
        {
            var result = await templateManager.AddLikeAsync(like);
            return RedirectToAction("Details", new { id = like.TemplateId });
        }

    }
}

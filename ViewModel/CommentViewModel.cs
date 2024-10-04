using ExamForms.Models;

namespace ExamForms.ViewModel
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }

        public int TemplateId { get; set; }
        public virtual Template Template { get; set; }

        public int UserId { get; set; }

        public string CommentText { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

using ExamForms.Models;

namespace ExamForms.ViewModel
{
    public class FormViewModel
    {
        public int FormId { get; set; }

        public int TemplateId { get; set; }
        public virtual Template Template { get; set; }

        public int UserId { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.Now;

        public virtual ICollection<Answer> Answers { get; set; }
    }
}

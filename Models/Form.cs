namespace ExamForms.Models
{
    public class Form
    {
        public int FormId { get; set; }

        public int TemplateId { get; set; }
        public virtual Template Template { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.Now;

        public virtual ICollection<Answer> Answers { get; set; }
    }
}

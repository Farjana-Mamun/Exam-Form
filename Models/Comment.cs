using System.ComponentModel.DataAnnotations;

namespace ExamForms.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public int TemplateId { get; set; }
        public virtual Template Template { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public string CommentText { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

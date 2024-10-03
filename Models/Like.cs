using System.ComponentModel.DataAnnotations;

namespace ExamForms.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }

        public int TemplateId { get; set; }
        public virtual Template Template { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime LikedAt { get; set; } = DateTime.Now;
    }
}

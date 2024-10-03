using ExamForms.Models;

namespace ExamForms.ViewModel
{
    public class LikeViewModel
    {
        public int LikeId { get; set; }

        public int TemplateId { get; set; }
        public virtual Template Template { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime LikedAt { get; set; } = DateTime.Now;
    }
}

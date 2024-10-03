using ExamForms.Models;
using System.ComponentModel.DataAnnotations;

namespace ExamForms.ViewModel
{
    public class TemplateViewModel
    {
        public int TemplateId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        [MaxLength(100)]
        public string Topic { get; set; }

        public string Tags { get; set; }

        [MaxLength(500)]
        public string ImageUrl { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Form> Forms { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}

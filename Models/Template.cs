using System.ComponentModel.DataAnnotations;

namespace ExamForms.Models
{
    public class Template
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

        public bool Public { get; set; } = false;

        public int CreatorId { get; set; }
        public virtual User Creator { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Form> Forms { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ExamForms.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        public int TemplateId { get; set; }
        public virtual Template Template { get; set; }

        [Required]
        [MaxLength(50)]
        public string QuestionType { get; set; } // SingleLine, MultiLine, Integer, Checkbox

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool DisplayInTable { get; set; } = false;

        public int? SortOrder { get; set; }
    }
}

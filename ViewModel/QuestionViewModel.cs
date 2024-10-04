using ExamForms.Models;
using System.ComponentModel.DataAnnotations;

namespace ExamForms.ViewModel
{
    public class QuestionViewModel
    {
        public int QuestionId { get; set; }

        public int TemplateId { get; set; } = 0;

        [Required]
        [MaxLength(50)]
        public string QuestionType { get; set; } // SingleLine, MultiLine, Integer, Checkbox

        [Required]
        [MaxLength(255)]
        public string QuestionTitle { get; set; }

        public string Description { get; set; }

        public bool DisplayInTable { get; set; } = false;

        public int? SortOrder { get; set; }
        public virtual Template Template { get; set; }
    }
}

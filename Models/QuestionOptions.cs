using System.ComponentModel.DataAnnotations;

namespace ExamForms.Models
{
    public class QuestionOptions
    {
        [Key]
        public int QuestionOptionsId { get; set; }
        public int QuestionId { get; set; }

        public string QuestionOption { get; set; }
        public bool IsCorrectAnswer { get; set; }
    }
}

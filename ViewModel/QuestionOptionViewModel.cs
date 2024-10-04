using ExamForms.Models;
using System.ComponentModel.DataAnnotations;

namespace ExamForms.ViewModel
{
    public class QuestionOptionViewModel
    {
        public int QuestionOptionId { get; set; }
        public int QuestionId { get; set; }

        public string OptionName { get; set; }
        public bool IsCorrectAnswer { get; set; }
    }
}

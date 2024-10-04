using ExamForms.Models;
using System.ComponentModel.DataAnnotations;

namespace ExamForms.ViewModel
{
    public class QuestionOptionsViewModel
    {
        public int QuestionOptionsId { get; set; }
        public int QuestionId { get; set; }

        public string QuestionOption { get; set; }
        public bool IsCorrectAnswer { get; set; }
    }
}

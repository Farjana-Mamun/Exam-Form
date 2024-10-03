namespace ExamForms.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }

        public int FormId { get; set; }
        public virtual Form Form { get; set; }

        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }

        public string AnswerText { get; set; }

        public int? AnswerInt { get; set; }
    }
}

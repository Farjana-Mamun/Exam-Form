using AutoMapper;
using ExamForms.Repository;

namespace ExamForms.Manager
{
    public class QuestionManager
    {
        private readonly QuestionRepository questionRepository;

        public QuestionManager(QuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }
    }
}

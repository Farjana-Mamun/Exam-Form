using AutoMapper;
using ExamForms.Models;
using ExamForms.Repository;
using ExamForms.ViewModel;
using System.Security.Principal;

namespace ExamForms.Manager
{
    public class QuestionManager
    {
        private readonly QuestionRepository questionRepository;
        private readonly Mapper mapper;

        public QuestionManager(QuestionRepository questionRepository
            , Mapper mapper)
        {
            this.questionRepository = questionRepository;
            this.mapper = mapper;
        }

        public async Task<List<QuestionViewModel>> GetAllQuestionsAsync()
        {
            try
            {
                List<Question> questions = await questionRepository.GetAllQuestions();
                List<QuestionViewModel> results = new List<QuestionViewModel>();
                return mapper.Map<List<QuestionViewModel>>(questions);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> AddTemplateQuestionAsync(QuestionViewModel model, IIdentity? User)
        {
            try
            {
                Question question = mapper.Map<Question>(model);
                return await questionRepository.AddTemplateQuestion(question);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

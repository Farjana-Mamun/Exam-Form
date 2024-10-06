using ExamForms.Data;
using ExamForms.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamForms.Repository
{
    public class QuestionRepository
    {
        private readonly ExamFormDbContext context;

        public QuestionRepository(ExamFormDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Question>> GetAllQuestions()
        {
            try
            {
                return await context.Questions.OrderBy(x => x.DisplayOrder).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> AddTemplateQuestion(Question model)
        {
            try
            {
                await context.Questions.AddAsync(model);
                return await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

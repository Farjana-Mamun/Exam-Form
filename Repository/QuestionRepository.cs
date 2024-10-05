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


    }
}

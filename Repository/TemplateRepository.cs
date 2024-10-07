using ExamForms.Data;
using ExamForms.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamForms.Repository
{
    public class TemplateRepository
    {
        private readonly ExamFormDbContext context;

        public TemplateRepository(ExamFormDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Template>> GetAllTemplate()
        {
            try
            {
                return await context.Templates.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Template> GetTemplateById(int id)
        {
            try
            {
                return await context.Templates.Where(x => x.TemplateId == id).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Topic>> GettAllTopic()
        {
            try
            {
                return await context.Topics.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<string>> GettAllTag()
        {
            try
            {
                return await context.Tags.Select(t => t.TagName).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> CreateTemplate(Template model, List<Tag> tags)
        {
            try
            {
                await context.Templates.AddAsync(model);
                await context.SaveChangesAsync();

                var unavailableTags = tags
                    .Where(tag => !context.Tags.Select(t => t.TagName).ToList()
                        .Contains(tag.TagName))
                    .ToList();

                if (unavailableTags.Any())
                {
                    await context.Tags.AddRangeAsync(unavailableTags);
                    await context.SaveChangesAsync();
                }
                return model.TemplateId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateTemplate(Template model, List<Tag> tags)
        {
            try
            {
                 context.Templates.Update(model);
                await context.SaveChangesAsync();

                var unavailableTags = tags
                    .Where(tag => !context.Tags.Select(t => t.TagName).ToList()
                        .Contains(tag.TagName))
                    .ToList();

                if (unavailableTags.Any())
                {
                    await context.Tags.AddRangeAsync(unavailableTags);
                    await context.SaveChangesAsync();
                }
                return model.TemplateId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteTemplate(int id)
        {
            try
            {
                Template template = await context.Templates.FindAsync(id);
                if (template != null)
                {
                    context.Templates.Remove(template);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

using AutoMapper;
using ExamForms.Data;
using ExamForms.Models;
using ExamForms.Repository;
using ExamForms.ViewModel;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace ExamForms.Manager
{
    public class TemplateManager
    {
        private readonly TemplateRepository templateRepository;
        private readonly IMapper mapper;

        public TemplateManager(TemplateRepository templateRepository
            , IMapper mapper)
        {
            this.templateRepository = templateRepository;
            this.mapper = mapper;
        }

        public async Task<List<TemplateViewModel>> GettAllTemplateAsync()
        {
            try
            {
                var templates = await templateRepository.GetAllTemplate();
                return mapper.Map<List<TemplateViewModel>>(templates);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TemplateViewModel> GetTemplateByIdAsync(int id)
        {
            try
            {
                var templates = await templateRepository.GetTemplateById(id);
                return mapper.Map<TemplateViewModel>(templates);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<TopicViewModel>> GettAllTopic()
        {
            try
            {
                var topics = await templateRepository.GettAllTopic();
                return mapper.Map<List<TopicViewModel>>(topics);
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
                var tags = await templateRepository.GettAllTag();
                return mapper.Map<List<string>>(tags);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> CreateTemplateAsync(TemplateViewModel model, IIdentity? User)
        {
            try
            {
                model.CreatedBy = User.Name;
                model.CreatedDate = DateTime.Now;

                List<Tag> tags = new List<Tag>();
                if (model.Tags != null)
                    tags = model.Tags.Split(',').Select(tag => new Tag { TagName = tag.Trim().ToLower() }).ToList();

                Template template = new Template();
                template = mapper.Map<Template>(model);
                return await templateRepository.CreateTemplate(template, tags);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateTemplateAsync(TemplateViewModel model, IIdentity? User)
        {
            try
            {
                model.CreatedBy = User.Name;
                model.CreatedDate = DateTime.Now;

                List<Tag> tags = new List<Tag>();
                if (model.Tags != null)
                    tags = model.Tags.Split(',').Select(tag => new Tag { TagName = tag.Trim().ToLower() }).ToList();

                Template template = new Template();
                template = mapper.Map<Template>(model);
                return await templateRepository.UpdateTemplate(template, tags);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task DeleteTemplateAsync(int id)
        {
            try
            {
                await templateRepository.DeleteTemplate(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<CommentViewModel>> GetCommentsByTemplateIdAsync(int templateId)
        {
            try
            {
                var comments = await templateRepository.GetCommentsByTemplateId(templateId);
                return mapper.Map<List<CommentViewModel>>(comments);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddCommentAsync(CommentViewModel comment)
        {
            try
            {
                var commentEntity = mapper.Map<Comment>(comment);
                return await templateRepository.AddComment(commentEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<LikeViewModel>> GetLikesByTemplateIdAsync(int templateId)
        {
            try
            {
                var likes = await templateRepository.GetLikesByTemplateId(templateId);
                return mapper.Map<List<LikeViewModel>>(likes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddLikeAsync(LikeViewModel like)
        {
            try
            {
                var likeEntity = mapper.Map<Like>(like);
                return await templateRepository.AddLike(likeEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamForms.ViewModel;

public partial class TemplateViewModel
{
    public int TemplateId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    [Display(Name = "Topic")]
    public int TopicId { get; set; }

    public string Tags { get; set; } = null!;

    public string Image { get; set; } = null!;

    [Display(Name = "Access Mode")]

    public string AccessMode { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();
    public virtual ICollection<TagViewModel> TagList { get; set; } = new List<TagViewModel>();

    public virtual ICollection<FormViewModel> Forms { get; set; } = new List<FormViewModel>();

    public virtual ICollection<LikeViewModel> Likes { get; set; } = new List<LikeViewModel>();

    public virtual ICollection<QuestionViewModel> Questions { get; set; } = new List<QuestionViewModel>();

    public virtual ICollection<TemplateSpecificUserViewModel> TemplateSpecificUsers { get; set; } = new List<TemplateSpecificUserViewModel>();
}

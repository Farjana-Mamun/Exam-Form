using System;
using System.Collections.Generic;

namespace ExamForms.Models;

public partial class Template
{
    public int TemplateId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int TopicId { get; set; }

    public string Tags { get; set; } = null!;

    public string Image { get; set; } = null!;

    public string AccessMode { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Form> Forms { get; set; } = new List<Form>();

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<TemplateSpecificUser> TemplateSpecificUsers { get; set; } = new List<TemplateSpecificUser>();
}

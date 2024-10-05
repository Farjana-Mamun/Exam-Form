using System;
using System.Collections.Generic;

namespace ExamForms.Models;

public partial class Question
{
    public int QuestionId { get; set; }

    public int TemplateId { get; set; }

    public string QuestionType { get; set; } = null!;
    public int SelectedOptionId { get; set; }

    public string QuestionTitle { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool IsDisplayed { get; set; }
    public int DisplayOrder { get; set; }

    public virtual ICollection<QuestionOption> QuestionOptions { get; set; } = new List<QuestionOption>();

    public virtual Template Template { get; set; } = null!;
}

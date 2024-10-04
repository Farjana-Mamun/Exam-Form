using System;
using System.Collections.Generic;

namespace ExamForms.Models;

public partial class Question
{
    public int QuestionId { get; set; }

    public int TemplateId { get; set; }

    public string QuestionType { get; set; } = null!;

    public string QuestionTitle { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool DisplayInTable { get; set; }

    public int? SortOrder { get; set; }

    public virtual ICollection<QuestionOption> QuestionOptions { get; set; } = new List<QuestionOption>();

    public virtual Template Template { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace ExamForms.ViewModel;

public partial class FormViewModel
{
    public int FormId { get; set; }

    public int TemplateId { get; set; }

    public int UserId { get; set; }

    public DateTime SubmittedAt { get; set; }

    public virtual ICollection<AnswerViewModel> Answers { get; set; } = new List<AnswerViewModel>();

    public virtual TemplateViewModel Template { get; set; } = null!;
}

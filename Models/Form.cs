using System;
using System.Collections.Generic;

namespace ExamForms.Models;

public partial class Form
{
    public int FormId { get; set; }

        [Required]
        [StringLength(255)]
        public string FormTitle { get; set; }

        public int TemplateId { get; set; }
        public virtual Template Template { get; set; }

    public int UserId { get; set; }

    public DateTime SubmittedAt { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual Template Template { get; set; } = null!;
}

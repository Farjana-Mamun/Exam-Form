﻿using System;
using System.Collections.Generic;

namespace ExamForms.Models;

public partial class Tag
{
    public int TagId { get; set; }

    public string? TagName { get; set; }

    public int? TemplateId { get; set; }
}

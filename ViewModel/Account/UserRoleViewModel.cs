﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamForms.ViewModel.Account
{
    public class UserRoleViewModel
    {
        public string? RoleId { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ExamForms.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required,MaxLength(100)]
        public string UserName { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public bool IsBlocked { get; set; } = false;

        [MaxLength(50)]
        public string Language { get; set; } = "en";

        [MaxLength(20)]
        public string Theme { get; set; } = "light";


    }
}

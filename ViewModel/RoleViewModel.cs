using ExamForms.Models;
using System.ComponentModel.DataAnnotations;

namespace ExamForms.ViewModel
{
    public class RoleViewModel
    {
        public int RoleId { get; set; }

        [Required]
        [MaxLength(100)]
        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}

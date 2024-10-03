using System.ComponentModel.DataAnnotations;

namespace ExamForms.Models
{
    public class Role
    {
        public int RoleId { get; set; }

        [Required]
        [MaxLength(100)]
        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
